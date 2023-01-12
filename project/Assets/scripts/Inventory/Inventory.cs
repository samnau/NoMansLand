using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();
    private void OnEnable()
    {
        Gem.OnGemCollected += Add;
    }
    private void OnDisable()
    {
        Gem.OnGemCollected -= Add;
    }
    public void Add(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item)){
            item.IncreaseStack();
            print($"there are {item.stackSize} {itemData.displayName}");
            OnInventoryChange?.Invoke(inventory);
        } else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            print($"adding {itemData.displayName} for the first time");
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.DecreaseStack();
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
    }
}
