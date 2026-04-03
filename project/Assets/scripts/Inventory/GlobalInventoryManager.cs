using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalInventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalInventoryState inventoryState;
    [SerializeField] private InventoryVisualDatabase visualDatabase;
    [SerializeField] private GameObject sharedTooltipPanel;
    [SerializeField] private TextMeshProUGUI sharedTooltipNameText;
    [SerializeField] private TextMeshProUGUI sharedTooltipDescriptionText;

    [Header("Items UI")]
    [SerializeField] private Transform itemsPanel;
    [SerializeField] private GameObject itemEntryPrefab;

    [Header("Familiars UI")]
    [SerializeField] private Transform familiarsPanel;
    [SerializeField] private GameObject familiarEntryPrefab;

    private void Awake()
    {
        // Initialize shared tooltip
        if (sharedTooltipPanel != null && sharedTooltipNameText != null && sharedTooltipDescriptionText != null)
        {
            GlobalInventoryEntryViewBase.InitializeSharedTooltip(sharedTooltipPanel, sharedTooltipNameText, sharedTooltipDescriptionText);
        }
    }

    private void OnEnable()
    {
        if (inventoryState != null)
        {
            inventoryState.InventoryChanged += RedrawAll;
            // Don't subscribe to OnActiveChanged to avoid redrawing on toggle changes
        }
        RedrawAll();
    }

    private void OnDisable()
    {
        if (inventoryState != null)
        {
            inventoryState.InventoryChanged -= RedrawAll;
        }

        // Save game when inventory UI is closed
        if (GlobalDataPersistenceManager.instance != null)
        {
            GlobalDataPersistenceManager.instance.SaveGame();
        }
    }

    private void RedrawAll()
    {
        Debug.Log("RedrawAll called");
        RedrawItems();
        RedrawFamiliars();
    }

    private void RedrawItems()
    {
        Debug.Log($"RedrawItems called, clearing {itemsPanel.childCount} children");
        if (itemsPanel == null)
        {
            return;
        }

        foreach (Transform child in itemsPanel)
        {
            Destroy(child.gameObject);
        }

        if (inventoryState == null || itemEntryPrefab == null)
        {
            return;
        }

        IReadOnlyList<GlobalGameData.InventoryItem> collectedItems = inventoryState.GetCollectedItems();
        for (int i = 0; i < collectedItems.Count; i++)
        {
            GameObject entry = Instantiate(itemEntryPrefab, itemsPanel, false);
            GlobalInventoryItemEntryView view = entry.GetComponent<GlobalInventoryItemEntryView>();
            if (view != null)
            {
                view.Bind(collectedItems[i], inventoryState, visualDatabase);
            }
        }
    }

    private void RedrawFamiliars()
    {
        Debug.Log($"RedrawFamiliars called, clearing {familiarsPanel.childCount} children");
        if (familiarsPanel == null)
        {
            return;
        }

        foreach (Transform child in familiarsPanel)
        {
            Destroy(child.gameObject);
        }

        if (inventoryState == null || familiarEntryPrefab == null)
        {
            return;
        }

        IReadOnlyList<GlobalGameData.Familiar> collectedFamiliars = inventoryState.GetCollectedFamiliars();
        for (int i = 0; i < collectedFamiliars.Count; i++)
        {
            GameObject entry = Instantiate(familiarEntryPrefab, familiarsPanel, false);
            GlobalFamiliarEntryView view = entry.GetComponent<GlobalFamiliarEntryView>();
            if (view != null)
            {
                view.Bind(collectedFamiliars[i], inventoryState, visualDatabase);
            }
        }
    }
}
