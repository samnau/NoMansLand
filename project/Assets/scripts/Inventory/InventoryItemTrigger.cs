using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemTrigger : MonoBehaviour
{
    InventoryConfirmationView inventoryConfirmationView;

    [Header("Item Configuration")]
    [SerializeField] private string itemId;
    [SerializeField] bool isCollectionTrigger = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetTriggerType();
            inventoryConfirmationView?.ShowConfirmationView(itemId);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inventoryConfirmationView?.HideConfirmationView();
        }
    }
}
