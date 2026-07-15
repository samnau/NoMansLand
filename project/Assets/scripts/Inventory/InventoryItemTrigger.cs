using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemTrigger : MonoBehaviour
{
    InventoryConfirmationView inventoryConfirmationView;

    [Header("Item Configuration")]
    [SerializeField] 
    [HideInInspector]
    private string itemId;
    public bool isCollectionTrigger = true;
    public bool disableOnComplete = false;
    [HideInInspector] public string completedDialog;
    GlobalInventoryState inventoryState;
    void Awake()
    {
        inventoryConfirmationView = FindObjectOfType<InventoryConfirmationView>(true);
        inventoryState = FindObjectOfType<GlobalInventoryState>();
    }

    private void Start()
    {
        OnCompleteHandler();
    }

    void SetTriggerType()
    {
        if(inventoryConfirmationView is null)
        {
            throw new System.InvalidOperationException("confirmation view not found");
        }
        inventoryConfirmationView.isCollectionConfirmation = isCollectionTrigger;
    }

    public void TriggerShowConfirmation()
    {
        SetTriggerType();
        inventoryConfirmationView?.ShowConfirmationView(itemId);
    }

    public void TriggerHideConfirmmation()
    {
        inventoryConfirmationView?.HideConfirmationView();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetTriggerType();
        }
    }

    public bool IsItemCollected()
    {
        //GlobalInventoryState inventoryState = FindObjectOfType<GlobalInventoryState>();
        if (inventoryState == null)
        {
            return false;
        }

        GlobalGameData.InventoryItem item = inventoryState.GetItemById(itemId);
        return item != null && item.collected;
    }

    public bool IsItemActive()
    {
        //GlobalInventoryState inventoryState = FindObjectOfType<GlobalInventoryState>();
        if (inventoryState == null)
        {
            return false;
        }

        GlobalGameData.InventoryItem item = inventoryState.GetItemById(itemId);
        return item != null && item.active;
    }

    public bool IsItemUsed()
    {
        //GlobalInventoryState inventoryState = FindObjectOfType<GlobalInventoryState>();
        if (inventoryState == null)
        {
            return false;
        }

        GlobalGameData.InventoryItem item = inventoryState.GetItemById(itemId);
        return item != null && item.used;
    }

    public void OnCompleteHandler()
    {
        print($"I should hide because - collected is:{IsItemCollected()}");
        if (disableOnComplete && (isCollectionTrigger ? IsItemCollected() : IsItemUsed()))
        {
            gameObject.SetActive(false);
        }
    }

}
