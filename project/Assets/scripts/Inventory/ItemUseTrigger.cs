using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseTrigger : MonoBehaviour
{
    [Header("Item Configuration")]
    [SerializeField] private string itemId;
    
    [Header("References")]
    [SerializeField] private GlobalInventoryState inventoryState;
    [SerializeField] private InventoryVisualDatabase visualDatabase;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject confirmationView;
    [SerializeField] private TextMeshProUGUI messageText;
    TextMeshProUGUI confirmationText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Image itemImage;
    
    [Header("Positioning")]
    [SerializeField] private float verticalOffset = 2f;
    
    private bool isShowingConfirmation = false;

    private void Awake()
    {
        if (confirmationView != null)
        {
            confirmationView.SetActive(false);
            confirmationText = confirmationView.GetComponentInChildren<TextMeshProUGUI>();
            print(confirmationText.name);
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

    public void TriggerUse()
    {
        if (string.IsNullOrEmpty(itemId) || inventoryState == null)
        {
            return;
        }

        if (IsItemUsed() || !IsItemActive())
        {
            return;
        }

        ShowConfirmationView();
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

    private bool IsItemUsed()
    {
        if (inventoryState == null || string.IsNullOrEmpty(itemId))
        {
            return false;
        }

        var item = inventoryState.GetItemById(itemId);
        return item != null && item.used;
    }

    private void ShowConfirmationView()
    {
        if (confirmationView == null || confirmationText == null)
        {
            return;
        }

        string itemName = GetItemDisplayName();
        confirmationText.text = $"Use <b>{itemName}</b>?";

        if (itemImage != null && visualDatabase != null)
        {
            Sprite icon = visualDatabase.GetItemIcon(itemId);
            itemImage.sprite = icon;
            itemImage.enabled = icon != null;
        }

        //PositionAbovePlayer();
        confirmationView.SetActive(true);
        isShowingConfirmation = true;
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

    private void PositionAbovePlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector3 playerPosition = player.transform.position;
        confirmationView.transform.position = new Vector3(playerPosition.x, playerPosition.y + verticalOffset, playerPosition.z);
    }

    private void OnYesClicked()
    {
        if (!string.IsNullOrEmpty(itemId) && inventoryState != null)
        {
            inventoryState.SetUsed(itemId, true);
            GlobalDataPersistenceManager.instance.SaveGame();
        }

        HideConfirmationView();
    }

    private void OnNoClicked()
    {
        HideConfirmationView();
    }

    private void HideConfirmationView()
    {
        if (confirmationView != null)
        {
            confirmationView.SetActive(false);
        }
        isShowingConfirmation = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && IsItemUsed() == false)
        {
            ShowConfirmationView();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HideConfirmationView();
        }
    }
}
