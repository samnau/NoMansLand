using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFamiliarEntryView : GlobalInventoryEntryViewBase
{
    //[SerializeField] private TextMeshProUGUI weaknessText;

    public void Bind(GlobalGameData.Familiar familiar, GlobalInventoryState state, InventoryVisualDatabase visualDatabase)
    {
        if (familiar == null)
            return;

        Sprite icon = visualDatabase?.GetFamiliarIcon(familiar.id);

        if (iconImage != null)
        {
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        BindBase(familiar.id, familiar.name, familiar.description, familiar.active, state, icon);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
