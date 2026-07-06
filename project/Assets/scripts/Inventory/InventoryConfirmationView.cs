using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryConfirmationView : MonoBehaviour
{
    [HideInInspector]
    public string itemId;

    [Header("References")]
    [SerializeField] private GlobalInventoryState inventoryState;
    [SerializeField] private InventoryVisualDatabase visualDatabase;
    [SerializeField] private GameObject confirmationView;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Image itemImage;
    [SerializeField] TextMeshProUGUI confirmationText;

    [Header("Pause Events")]
    [SerializeField] GameEvent freezeEvent;
    [SerializeField] GameEvent unfreezeEvent;

    [Header("Item Use Event")]
    [SerializeField] GameEvent confirmationEvent;

    [HideInInspector]
    public bool isCollectionConfirmation = true;
    private bool isShowingConfirmation = false;

    PositionTweener positionTweener;
    float transitionDuration = 0.3f;

    private void Awake()
    {
        if (confirmationView != null)
        {
            //confirmationView.SetActive(false);
            //confirmationText = confirmationView.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (yesButton != null)
        {
            yesButton.onClick.AddListener(OnYesClicked);
        }

        if (noButton != null)
        {
            noButton.onClick.AddListener(OnNoClicked);
        }
        positionTweener = GetComponent<PositionTweener>();
        if(positionTweener != null)
        {
            positionTweener.MoveUIUpward(0);
        }
    }

    private bool IsItemCollected()
    {
        if (inventoryState == null || string.IsNullOrEmpty(itemId))
        {
            return false;
        }

        var item = inventoryState.GetItemById(itemId);
        return item != null && item.collected;
    }

    private bool IsItemUsed()
    {
        if (inventoryState == null || string.IsNullOrEmpty(itemId))
        {
            return false;
        }

        var item = inventoryState.GetItemById(itemId);
        print($"item is used:{item.used}");

        return item != null && item.used;
    }

    private bool IsItemActive()
    {
        if (inventoryState == null || string.IsNullOrEmpty(itemId))
        {
            return false;
        }

        var item = inventoryState.GetItemById(itemId);
        print($"item is active:{item.active}");
        return item != null && item.active;
    }

    private void OnDestroy()
    {
        if (yesButton != null)
        {
            yesButton.onClick.RemoveListener(OnYesClicked);
        }

        if (noButton != null)
        {
            noButton.onClick.RemoveListener(OnNoClicked);
        }
    }

    public void BroadcastItemUse()
    {
        confirmationEvent?.Invoke();
    }

    private void OnYesClicked()
    {
        if (!string.IsNullOrEmpty(itemId) && inventoryState != null)
        {

            if(isCollectionConfirmation)
            {
                inventoryState.SetCollected(itemId, true);
            }
            else
            {
                inventoryState.SetUsed(itemId, true);
                inventoryState.SetActive(itemId, false);
                BroadcastItemUse();
            }

            GlobalDataPersistenceManager.instance.SaveGame();
        }

        HideConfirmationView();
    }

    private void OnNoClicked()
    {
        HideConfirmationView();
    }

    string GetConfirmationText(string itemName)
    {
        string messagePrefix = isCollectionConfirmation ? "Pick up" : "Use";
        return $"{messagePrefix} <b>{itemName}</b>?";
    }

    private string GetItemDisplayName()
    {
        if (inventoryState == null || string.IsNullOrEmpty(itemId))
        {
            Debug.LogWarning("No inventory item found.");
            return "item";
        }
        var item = inventoryState.GetItemById(itemId);
        return item != null ? item.name : "item";
    }

    void FreezePlayer()
    {
        if(freezeEvent is null)
        {
            Debug.LogWarning("No freeze event assigned");
        }
        freezeEvent?.Invoke();
    }

    void UnfreezePlayer()
    {
        if (freezeEvent is null)
        {
            Debug.LogWarning("No unfreeze event assigned");
        }
        unfreezeEvent?.Invoke();
    }

    public void HideConfirmationView()
    {
        if (confirmationView != null)
        {
            positionTweener?.MoveUIUpward(transitionDuration);
        }
        isShowingConfirmation = false;
        UnfreezePlayer();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ShowConfirmationView(string itemIdString)
    {
        print($"item id: {itemIdString}");
        itemId = itemIdString;
        if (confirmationView == null || confirmationText == null)
        {
            return;
        }

        if(isCollectionConfirmation && IsItemCollected())
        {
            UnfreezePlayer();
            return;

        } else if (!isCollectionConfirmation && !IsItemActive() || IsItemUsed())
        {
            UnfreezePlayer();
            return;
        }

        string itemName = GetItemDisplayName();
        confirmationText.text = GetConfirmationText(itemName);

        if (itemImage != null && visualDatabase != null)
        {
            Sprite icon = visualDatabase.GetItemIcon(itemId);
            itemImage.sprite = icon;
            itemImage.enabled = icon != null;
        }

        //confirmationView.SetActive(true);
        positionTweener.MoveUIDownward(transitionDuration);
        isShowingConfirmation = true;
    }
}
