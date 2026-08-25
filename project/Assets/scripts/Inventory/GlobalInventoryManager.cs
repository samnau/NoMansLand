using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlobalInventoryManager : MonoBehaviour
{
    InventorySoundFX inventorySoundFX;

    [Header("References")]
    [SerializeField] private GlobalInventoryState inventoryState;
    [SerializeField] private InventoryVisualDatabase visualDatabase;
    [SerializeField] private GameObject sharedTooltipPanel;
    [SerializeField] private TextMeshProUGUI sharedTooltipNameText;
    [SerializeField] private TextMeshProUGUI sharedTooltipDescriptionText;
    [SerializeField] private Image sharedTooltipImage;

    [Header("Items UI")]
    [SerializeField] private Transform itemsPanel;
    [SerializeField] private GameObject itemEntryPrefab;
    [SerializeField] private ToggleGroup itemsToggleGroup;

    [Header("Familiars UI")]
    [SerializeField] private Transform familiarsPanel;
    [SerializeField] private GameObject familiarEntryPrefab;
    [SerializeField] private ToggleGroup familiarsToggleGroup;

    [Header("Inventory Elements")]
    [SerializeField] ColorTweener backgroundShadeTweener;
    [SerializeField] GameObject inventoryWrapper;
    [SerializeField] Button closeButton;
    [SerializeField] Animator keyChainAnimator;

    [Header("Inventory Events")]
    [SerializeField] GameEvent freezePlayerEvent;
    [SerializeField] GameEvent unfreezePlayerEvent;

    bool inventoryInMotion = false;
    bool inventoryVisible = false;
    bool inventoryDisabled = false;
    PositionTweener positionTweener;
    ScaleTweener scaleTweener;
    float transitionDuration = 0.75f;

    private void Awake()
    {
        // Initialize shared tooltip
        if (sharedTooltipPanel != null && sharedTooltipNameText != null && sharedTooltipDescriptionText != null)
        {
            GlobalInventoryEntryViewBase.InitializeSharedTooltip(sharedTooltipPanel, sharedTooltipNameText, sharedTooltipDescriptionText, sharedTooltipImage);
        }
    }

    private void Start()
    {
        if(inventoryWrapper != null)
        {
            positionTweener = inventoryWrapper.GetComponent<PositionTweener>();
            positionTweener.MoveUIBackward(0f);
            scaleTweener = inventoryWrapper.GetComponent<ScaleTweener>();
        }

        if(closeButton != null)
        {
            closeButton.onClick.AddListener(CloseClickHandler);
        }
        inventorySoundFX = GetComponent<InventorySoundFX>();
    }

    void DisableInventory()
    {
        inventoryDisabled = true;
    }

    void EnableInventory()
    {
        inventoryDisabled = false;
    }

    void CloseClickHandler ()
    {
        if (inventoryWrapper != null)
        {
            ToggleInventoryDisplay();
            inventoryVisible = false;
        }
    }

    private void OnEnable()
    {
        if (inventoryState != null)
        {
            inventoryState.InventoryChanged += RedrawAll;
            // Don't subscribe to OnActiveChanged to avoid redrawing on toggle changes
        }
        RedrawAll();
    }

    private void OnDisable()
    {
        if (inventoryState != null)
        {
            inventoryState.InventoryChanged -= RedrawAll;
        }

        // Save game when inventory UI is closed
        if (GlobalDataPersistenceManager.instance != null)
        {
            GlobalDataPersistenceManager.instance.SaveGame();
        }
    }

    private void RedrawAll()
    {
        RedrawItems();
        RedrawFamiliars();
    }

    private void RedrawItems()
    {
        if (itemsPanel == null)
        {
            return;
        }

        foreach (Transform child in itemsPanel)
        {
            Destroy(child.gameObject);
        }

        if (inventoryState == null || itemEntryPrefab == null)
        {
            return;
        }

        IReadOnlyList<GlobalGameData.InventoryItem> collectedItems = inventoryState.GetCollectedItems();
        for (int i = 0; i < collectedItems.Count; i++)
        {
            GameObject entry = Instantiate(itemEntryPrefab, itemsPanel, false);
            GlobalInventoryItemEntryView view = entry.GetComponent<GlobalInventoryItemEntryView>();
            if (view != null)
            {
                view.Bind(collectedItems[i], inventoryState, visualDatabase);

                // Assign the ToggleGroup to the item's toggle
                if (view.activeToggle != null && itemsToggleGroup != null)
                {
                    view.activeToggle.group = itemsToggleGroup;
                }
            }
        }
    }

    void ToggleInventoryShade(bool show = true)
    {
        float targetAlpha = show ? 0.5f : 0f;
        backgroundShadeTweener.TriggerImageAlphaByDuration(targetAlpha, 0.5f);
    }

    void ToggleInventoryDisplay()
    {
        if(inventoryInMotion)
        {
            return;
        }
        if(positionTweener != null)
        {
            if(inventoryVisible)
            {
                positionTweener.MoveUIBackward(transitionDuration);
                ToggleInventoryShade(false);
                inventorySoundFX?.PlayHide();
                UnfreezePlayer();
            } else
            {
                ToggleInventoryShade(true);
                inventorySoundFX?.PlayShow();
                positionTweener.MoveUIForward(transitionDuration);
                FreezePlayer();
            }
            StartCoroutine(ToggleKeyChain());
            StartCoroutine(InventoryTransition());
            StartCoroutine(InventoryToggleGuard(transitionDuration));
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    IEnumerator InventoryTransition()
    {
        scaleTweener.TriggerNonuniformScaleTween(1.05f, .95f, transitionDuration/2);
        yield return new WaitForSeconds(transitionDuration / 2);
        scaleTweener.TriggerUniformScaleTween(1f, transitionDuration / 2);
    }
    IEnumerator ToggleKeyChain()
    {
        if(inventoryVisible)
        {
            keyChainAnimator?.SetBool("MOVE_OUT", true);
        }
        yield return new WaitForSeconds(.5f);
        if (!inventoryVisible)
        {
            keyChainAnimator?.SetBool("MOVE_IN", true);
        }
        yield return new WaitForSeconds(1.5f);
        keyChainAnimator?.SetBool("MOVE_IN", false);
        keyChainAnimator?.SetBool("MOVE_OUT", false);
    }

    IEnumerator InventoryToggleGuard(float duration)
    {
        inventoryInMotion = true;
        yield return new WaitForSeconds(duration);
        inventoryInMotion = false;
        inventoryVisible = positionTweener.IsUiRectVisible(closeButton.GetComponent<RectTransform>());
    }

    private void RedrawFamiliars()
    {
        if (familiarsPanel == null)
        {
            return;
        }

        foreach (Transform child in familiarsPanel)
        {
            Destroy(child.gameObject);
        }

        if (inventoryState == null || familiarEntryPrefab == null)
        {
            return;
        }

        IReadOnlyList<GlobalGameData.Familiar> collectedFamiliars = inventoryState.GetCollectedFamiliars();
        for (int i = 0; i < collectedFamiliars.Count; i++)
        {
            GameObject entry = Instantiate(familiarEntryPrefab, familiarsPanel, false);
            GlobalFamiliarEntryView view = entry.GetComponent<GlobalFamiliarEntryView>();
            if (view != null)
            {
                view.Bind(collectedFamiliars[i], inventoryState, visualDatabase);

                // Assign the ToggleGroup to the familiar's toggle
                if (view.activeToggle != null && familiarsToggleGroup != null)
                {
                    view.activeToggle.group = familiarsToggleGroup;
                }
            }
        }
    }

    void FreezePlayer()
    {
        freezePlayerEvent?.Invoke();
    }

    void UnfreezePlayer()
    {
        unfreezePlayerEvent?.Invoke();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryDisplay();
        }
    }
}

