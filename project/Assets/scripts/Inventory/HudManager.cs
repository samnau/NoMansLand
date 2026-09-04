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
    [SerializeField] Image familiarImage;
    [SerializeField] TextMeshProUGUI familiarText;
    [SerializeField] GlobalInventoryState globalInventoryState;
    [SerializeField] InventoryVisualDatabase inventoryVisualDatabase;

    DialogManager dialogManager;

    private void OnEnable()
    {
        if (globalInventoryState != null)
        {
            GlobalInventoryState.OnActiveChanged += UpdateActiveItemDisplay;
            GlobalInventoryState.OnActiveChanged += UpdateActiveFamiliarDisplay;
        }
    }

    private void OnDisable()
    {
        if (globalInventoryState != null)
        {
            GlobalInventoryState.OnActiveChanged -= UpdateActiveItemDisplay;
            GlobalInventoryState.OnActiveChanged -= UpdateActiveFamiliarDisplay;
        }
    }

    private void Start()
    {
        UpdateActiveItemDisplay();
        UpdateActiveFamiliarDisplay();
        dialogManager = FindAnyObjectByType<DialogManager>();
        if (dialogManager != null && dialogManager.isCutScene)
        {
            gameObject.SetActive(false);
        }
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

    private void UpdateActiveFamiliarDisplay()
    {
        if (familiarImage == null || globalInventoryState == null || inventoryVisualDatabase == null)
        {
            return;
        }

        var collectedFamiliars = globalInventoryState.GetCollectedFamiliars();
        var activeFamiliar = collectedFamiliars.FirstOrDefault(f => f.active);

        if (activeFamiliar == null || activeFamiliar.used)
        {
            familiarImage.enabled = false;
            if (familiarText != null)
            {
                familiarText.enabled = false;
            }
        }
        else
        {
            Sprite familiarIcon = inventoryVisualDatabase.GetFamiliarIcon(activeFamiliar.id);
            if (familiarIcon != null)
            {
                familiarImage.sprite = familiarIcon;
                familiarImage.enabled = true;
            }
            else
            {
                familiarImage.enabled = false;
            }

            if (familiarText != null)
            {
                familiarText.text = activeFamiliar.name;
                familiarText.enabled = true;
            }
        }
    }
}
