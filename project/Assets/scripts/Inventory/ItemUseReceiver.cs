using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseReceiver : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]
    [HideInInspector]
    List<string> requiredItems;
    [SerializeField]
    GameEvent completionEvent;
    
    private GlobalInventoryState inventoryState;

    void Start()
    {
        inventoryState = FindObjectOfType<GlobalInventoryState>();

        if(inventoryState is null)
        {
            Debug.LogWarning("No inventory state found");
        }
    }

    // Listen for a game event meant for general item use triggers
    public void CheckForRequiredItems()
    {
        if (inventoryState == null || requiredItems == null || requiredItems.Count == 0)
        {
            return;
        }

        bool allItemsUsed = true;
        
        foreach (string itemId in requiredItems)
        {
            GlobalGameData.InventoryItem item = inventoryState.GetItemById(itemId);
            if (item == null || !item.used)
            {
                allItemsUsed = false;
                break;
            }
        }
        print("CheckForRequiredItems is fired");
        print(allItemsUsed);
        if (allItemsUsed && completionEvent != null)
        {
            completionEvent.Invoke();
            print("all required event items used");
        }
    }

}
