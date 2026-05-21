using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [Header("Item Configuration")]
    [SerializeField] private string itemId;
    
    [Header("References")]
    [SerializeField] private GlobalInventoryState inventoryState;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject confirmationView;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    [Header("Positioning")]
    [SerializeField] private float verticalOffset = 2f;
    
    private bool isShowingConfirmation = false;

    private void Awake()
    {
        if (confirmationView != null)
        {
            confirmationView.SetActive(false);
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

    public void TriggerCollection()
    {
        if (string.IsNullOrEmpty(itemId) || inventoryState == null)
        {
            return;
        }

        if (IsItemCollected())
        {
            return;
        }

        ShowConfirmationView();
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

    private void ShowConfirmationView()
    {
        if (confirmationView == null || messageText == null)
        {
            return;
        }

        string itemName = GetItemDisplayName();
        messageText.text = $"Pick up {itemName}?";

        PositionAbovePlayer();
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
            inventoryState.SetCollected(itemId, true);
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
        if(collision.tag == "Player" && IsItemCollected() == false)
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
