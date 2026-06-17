using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlobalInventoryManager : MonoBehaviour
{
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
    [SerializeField] GameObject inventoryWrapper;
    [SerializeField] Button closeButton;

    [Header("Inventory Events")]
    [SerializeField] GameEvent freezePlayerEvent;
    [SerializeField] GameEvent unfreezePlayerEvent;

    bool inventoryInMotion = false;
    bool inventoryVisible = true;
    PositionTweener positionTweener;
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
            ToggleInventoryDisplay();
            UnfreezePlayer();
        }

        if(closeButton != null)
        {
            closeButton.onClick.AddListener(CloseClickHandler);
        }
    }

    void CloseClickHandler ()
    {
        if (inventoryWrapper != null)
        {
            positionTweener.MoveUIBackward(transitionDuration);
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
                UnfreezePlayer();
            } else
            {
                positionTweener.MoveUIForward(transitionDuration);
                FreezePlayer();
            }
            inventoryVisible = !inventoryVisible;
            StartCoroutine(InventoryToggleGuard(transitionDuration));
        }
    }

    IEnumerator InventoryToggleGuard(float duration)
    {
        inventoryInMotion = true;
        yield return new WaitForSeconds(duration);
        inventoryInMotion = false;
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
