using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GlobalInventoryEntryViewBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image iconImage;
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    public Toggle activeToggle;

    protected static GameObject sharedTooltipPanel;
    protected static TextMeshProUGUI sharedNameText;
    protected static TextMeshProUGUI sharedDescriptionText;
    protected static Image sharedTooltipImage;
    protected static RectTransform canvasRectTransform;
    protected static RectTransform tooltipRectTransform;

    protected string boundId;
    protected string boundName;
    protected string boundDescription;
    protected Sprite boundSprite;
    protected GlobalInventoryState inventoryState;

    public static void InitializeSharedTooltip(GameObject panel, TextMeshProUGUI nameText, TextMeshProUGUI descriptionText, Image image)
    {
        sharedTooltipPanel = panel;
        sharedNameText = nameText;
        sharedDescriptionText = descriptionText;
        sharedTooltipImage = image;
        
        // Cache canvas and tooltip RectTransforms for screen conversion
        Canvas canvas = panel.GetComponentInParent<Canvas>();
        if (canvas != null)
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        
        tooltipRectTransform = panel.GetComponent<RectTransform>();
    }

    protected void BindBase(string id, string name, string description, bool active, GlobalInventoryState state, Sprite sprite = null)
    {
        boundId = id;
        boundName = name;
        boundDescription = description;
        boundSprite = sprite;
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
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    void SetToolTipPosition()
    {
        if (sharedTooltipPanel == null)
            return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Use a fixed depth
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        sharedTooltipPanel.transform.position = worldPosition;
    }

    protected virtual void ShowTooltip()
    {
        if (sharedTooltipPanel == null || sharedNameText == null || sharedDescriptionText == null)
            return;

        // Set content
        sharedNameText.text = boundName;
        sharedDescriptionText.text = boundDescription;
        
        if (sharedTooltipImage != null)
        {
            sharedTooltipImage.sprite = boundSprite;
            sharedTooltipImage.enabled = boundSprite != null;
        }
        
        SetToolTipPosition();

        sharedTooltipPanel.SetActive(true);
    }

    protected virtual void Update()
    {
        if (sharedTooltipPanel != null && sharedTooltipPanel.activeSelf)
        {
            SetToolTipPosition();
        }
    }

    private void Awake()
    {
        HideTooltip();
    }

    protected virtual void HideTooltip()
    {
        if (sharedTooltipPanel != null)
            sharedTooltipPanel.SetActive(false);
    }

    private void OnActiveToggleChanged(bool isOn)
    {
        if (inventoryState == null || string.IsNullOrEmpty(boundId))
            return;

        inventoryState.SetActive(boundId, isOn);
    }
}
