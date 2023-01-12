using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize = 0;

    public InventoryItem(ItemData item)
    {
        itemData = item;
        IncreaseStack();
    }

    public void IncreaseStack()
    {
        stackSize++;
    }
    public void DecreaseStack()
    {
        stackSize--;
        if(stackSize<0)
        {
            stackSize = 0;
        }
    }
}
