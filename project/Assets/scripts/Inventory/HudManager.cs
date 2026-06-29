using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] GlobalInventoryState globalInventoryState;
    [SerializeField] InventoryVisualDatabase inventoryVisualDatabase;
    
    private void OnEnable()
    {
        if (globalInventoryState != null)
        {
            GlobalInventoryState.OnActiveChanged += UpdateActiveItemDisplay;
        }
    }

    private void OnDisable()
    {
        if (globalInventoryState != null)
        {
            GlobalInventoryState.OnActiveChanged -= UpdateActiveItemDisplay;
        }
    }

    private void Start()
    {
        UpdateActiveItemDisplay();
    }

    private void UpdateActiveItemDisplay()
    {
        if (itemImage == null || globalInventoryState == null || inventoryVisualDatabase == null)
        {
            return;
        }

        var collectedItems = globalInventoryState.GetCollectedItems();
        var activeItem = collectedItems.FirstOrDefault(i => i.active);

        if (activeItem == null || activeItem.used)
        {
            itemImage.enabled = false;
            if (itemText != null)
            {
                itemText.enabled = false;
            }
        }
        else
        {
            Sprite itemIcon = inventoryVisualDatabase.GetItemIcon(activeItem.id);
            if (itemIcon != null)
            {
                itemImage.sprite = itemIcon;
                itemImage.enabled = true;
            }
            else
            {
                itemImage.enabled = false;
            }

            if (itemText != null)
            {
                itemText.text = activeItem.name;
                itemText.enabled = true;
            }
        }
    }
}
