using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalInventoryItemEntryView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Toggle activeToggle;

    private string boundId;
    private GlobalInventoryState inventoryState;

    public void Bind(GlobalGameData.InventoryItem item, GlobalInventoryState state, InventoryVisualDatabase visualDatabase)
    {
        if (item == null)
        {
            return;
        }

        boundId = item.id;
        inventoryState = state;

        if (iconImage != null)
        {
            Sprite icon = visualDatabase?.GetItemIcon(item.id);
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        if (nameText != null)
        {
            nameText.text = item.name;
        }

        if (descriptionText != null)
        {
            descriptionText.text = item.description;
        }

        if (activeToggle != null)
        {
            activeToggle.onValueChanged.RemoveListener(OnActiveToggleChanged);
            activeToggle.isOn = item.active;
            activeToggle.onValueChanged.AddListener(OnActiveToggleChanged);
        }
    }

    private void OnDisable()
    {
        if (activeToggle != null)
        {
            activeToggle.onValueChanged.RemoveListener(OnActiveToggleChanged);
        }
    }

    private void OnActiveToggleChanged(bool isOn)
    {
        if (inventoryState == null || string.IsNullOrEmpty(boundId))
        {
            return;
        }

        inventoryState.SetActive(boundId, isOn);
    }
}
