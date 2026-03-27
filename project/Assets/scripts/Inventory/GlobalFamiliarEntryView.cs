using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFamiliarEntryView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI weaknessText;
    [SerializeField] private Toggle activeToggle;

    private string boundId;
    private GlobalInventoryState inventoryState;

    public void Bind(GlobalGameData.Familiar familiar, GlobalInventoryState state, InventoryVisualDatabase visualDatabase)
    {
        if (familiar == null)
        {
            return;
        }

        boundId = familiar.id;
        inventoryState = state;

        if (iconImage != null)
        {
            Sprite icon = visualDatabase?.GetFamiliarIcon(familiar.id);
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        if (nameText != null)
        {
            nameText.text = familiar.name;
        }

        if (descriptionText != null)
        {
            descriptionText.text = familiar.description;
        }

        if (weaknessText != null)
        {
            weaknessText.text = familiar.weakness;
        }

        if (activeToggle != null)
        {
            activeToggle.onValueChanged.RemoveListener(OnActiveToggleChanged);
            activeToggle.isOn = familiar.active;
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
