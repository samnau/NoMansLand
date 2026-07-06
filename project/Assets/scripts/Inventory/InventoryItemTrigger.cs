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
    [SerializeField] bool isCollectionTrigger = true;
    [SerializeField] GameEvent confirmationEvent;
    public string collectedDialog;
    void Awake()
    {
        inventoryConfirmationView = FindObjectOfType<InventoryConfirmationView>(true);
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

    public void TriggerConfirmationEvent()
    {
        confirmationEvent?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetTriggerType();
        }
    }

}
