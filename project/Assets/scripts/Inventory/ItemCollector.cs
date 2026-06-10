using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    InventoryConfirmationView inventoryConfirmationView;

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
        //if (confirmationView != null)
        //{
        //    confirmationView.SetActive(false);
        //    confirmationText = confirmationView.GetComponentInChildren<TextMeshProUGUI>();
        //}

        //if (yesButton != null)
        //{
        //    yesButton.onClick.AddListener(OnYesClicked);
        //}

        //if (noButton != null)
        //{
        //    noButton.onClick.AddListener(OnNoClicked);
        //}
        inventoryConfirmationView = FindObjectOfType<InventoryConfirmationView>(true);
    }

    private void Start()
    {
       // inventoryConfirmationView = FindObjectOfType<InventoryConfirmationView>(true);
        //inventoryConfirmationView = confirmationView.GetComponent<InventoryConfirmationView>();
    }

    //private void OnDestroy()
    //{
    //    if (yesButton != null)
    //    {
    //        yesButton.onClick.RemoveListener(OnYesClicked);
    //    }

    //    if (noButton != null)
    //    {
    //        noButton.onClick.RemoveListener(OnNoClicked);
    //    }
    //}

    //public void TriggerCollection()
    //{
    //    if (string.IsNullOrEmpty(itemId) || inventoryState == null)
    //    {
    //        return;
    //    }

    //    if (IsItemCollected())
    //    {
    //        return;
    //    }

    //    ShowConfirmationView();
    //}

    //private bool IsItemCollected()
    //{
    //    if (inventoryState == null || string.IsNullOrEmpty(itemId))
    //    {
    //        return false;
    //    }

    //    var item = inventoryState.GetItemById(itemId);
    //    return item != null && item.collected;
    //}

    //private void ShowConfirmationView()
    //{
    //    if (confirmationView == null || confirmationText == null)
    //    {
    //        return;
    //    }

    //    string itemName = GetItemDisplayName();
    //    confirmationText.text = $"Pick up <b>{itemName}</b>?";

    //    if (itemImage != null && visualDatabase != null)
    //    {
    //        Sprite icon = visualDatabase.GetItemIcon(itemId);
    //        itemImage.sprite = icon;
    //        itemImage.enabled = icon != null;
    //    }

    //    //PositionAbovePlayer();
    //    confirmationView.SetActive(true);
    //    isShowingConfirmation = true;
    //}

    //private string GetItemDisplayName()
    //{
    //    if (inventoryState == null || string.IsNullOrEmpty(itemId))
    //    {
    //        return "item";
    //    }

    //    var item = inventoryState.GetItemById(itemId);
    //    return item != null ? item.name : "item";
    //}

    //private void PositionAbovePlayer()
    //{
    //    if (player == null)
    //    {
    //        return;
    //    }

    //    Vector3 playerPosition = player.transform.position;
    //    confirmationView.transform.position = new Vector3(playerPosition.x, playerPosition.y + verticalOffset, playerPosition.z);
    //}

    //private void OnYesClicked()
    //{
    //    if (!string.IsNullOrEmpty(itemId) && inventoryState != null)
    //    {
    //        inventoryState.SetCollected(itemId, true);
    //        GlobalDataPersistenceManager.instance.SaveGame();
    //    }

    //    HideConfirmationView();
    //}

    //private void OnNoClicked()
    //{
    //    HideConfirmationView();
    //}

    //private void HideConfirmationView()
    //{
    //    if (confirmationView != null)
    //    {
    //        confirmationView.SetActive(false);
    //    }
    //    isShowingConfirmation = false;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //ShowConfirmationView();
            inventoryConfirmationView?.ShowConfirmationView(itemId);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //HideConfirmationView();
            inventoryConfirmationView?.HideConfirmationView();
        }
    }
}
