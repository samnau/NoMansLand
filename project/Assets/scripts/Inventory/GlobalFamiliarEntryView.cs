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
        print("familiar is not null");
        if (iconImage != null)
        {
            Sprite icon = visualDatabase?.GetFamiliarIcon(familiar.id);
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        //if (weaknessText != null)
        //    weaknessText.text = familiar.weakness;

        BindBase(familiar.id, familiar.name, familiar.description, familiar.active, state);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log($"Familiar entry disabled: {boundId}");
    }

    private void Awake()
    {
        Debug.Log($"Familiar entry awake: {boundId}");
        Debug.Log($"Familiar entry Start: enabled={enabled}");
    }
}
