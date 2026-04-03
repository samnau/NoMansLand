using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GlobalInventoryEntryViewBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image iconImage;
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    [SerializeField] protected Toggle activeToggle;
    [SerializeField] protected GameObject tooltipPanel;

    protected string boundId;
    protected GlobalInventoryState inventoryState;

    protected void BindBase(string id, string name, string description, bool active, GlobalInventoryState state)
    {
        boundId = id;
        inventoryState = state;

        if (nameText != null)
            nameText.text = name;

        if (descriptionText != null)
            descriptionText.text = description;

        if (activeToggle != null)
        {
            activeToggle.onValueChanged.RemoveListener(OnActiveToggleChanged);
            activeToggle.isOn = active;
            activeToggle.onValueChanged.AddListener(OnActiveToggleChanged);
        }

        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        if (activeToggle != null)
        {
            activeToggle.onValueChanged.RemoveListener(OnActiveToggleChanged);
        }

        HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    protected virtual void ShowTooltip()
    {
        if (tooltipPanel != null)
            tooltipPanel.SetActive(true);
    }

    protected virtual void HideTooltip()
    {
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }

    private void OnActiveToggleChanged(bool isOn)
    {
        if (inventoryState == null || string.IsNullOrEmpty(boundId))
            return;

        inventoryState.SetActive(boundId, isOn);
    }
}
