using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI stackSizeText;
    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        stackSizeText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if(item == null)
        {
            ClearSlot();
            return;
        }

        InitSlot();
        icon.sprite = item.itemData.icon;
        labelText.text = item.itemData.displayName;
        stackSizeText.text = item.stackSize.ToString();
    }

    public void InitSlot()
    {
        icon.enabled = true;
        labelText.enabled = true;
        stackSizeText.enabled = true;
    }
}
