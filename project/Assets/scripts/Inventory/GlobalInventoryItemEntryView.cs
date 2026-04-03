using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalInventoryItemEntryView : GlobalInventoryEntryViewBase
{
    public void Bind(GlobalGameData.InventoryItem item, GlobalInventoryState state, InventoryVisualDatabase visualDatabase)
    {
        if (item == null)
            return;

        if (iconImage != null)
        {
            Sprite icon = visualDatabase?.GetItemIcon(item.id);
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        BindBase(item.id, item.name, item.description, item.active, state);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log($"Item entry disabled: {boundId}");
    }
}
