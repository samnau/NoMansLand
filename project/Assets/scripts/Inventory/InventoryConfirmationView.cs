using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isCollectionConfirmation = true;
    private bool isShowingConfirmation = false;

    private void Awake()
    {
        if (confirmationView != null)
        {
            confirmationView.SetActive(false);
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
        return item != null && item.used;
    }

    private bool IsItemActive()
    {
        if (inventoryState == null || string.IsNullOrEmpty(itemId))
        {
            return false;
        }

        var item = inventoryState.GetItemById(itemId);
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
            return "item";
        }

        var item = inventoryState.GetItemById(itemId);
        return item != null ? item.name : "item";
    }

    public void HideConfirmationView()
    {
        if (confirmationView != null)
        {
            confirmationView.SetActive(false);
        }
        isShowingConfirmation = false;
    }

    public void ShowConfirmationView(string itemIdString)
    {
        print("new confirmation code: show");
        itemId = itemIdString;
        if (confirmationView == null || confirmationText == null)
        {
            return;
        }

        if(isCollectionConfirmation && IsItemCollected())
        {
            return;

        } else if (IsItemActive() && !IsItemUsed())
        {
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

        confirmationView.SetActive(true);
        isShowingConfirmation = true;
    }
}
