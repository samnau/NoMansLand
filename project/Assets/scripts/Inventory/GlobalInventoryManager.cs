using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalInventoryState inventoryState;
    [SerializeField] private InventoryVisualDatabase visualDatabase;

    [Header("Items UI")]
    [SerializeField] private Transform itemsPanel;
    [SerializeField] private GameObject itemEntryPrefab;

    [Header("Familiars UI")]
    [SerializeField] private Transform familiarsPanel;
    [SerializeField] private GameObject familiarEntryPrefab;

    private void OnEnable()
    {
        if (inventoryState == null)
        {
            inventoryState = FindFirstObjectByType<GlobalInventoryState>();
        }

        if (inventoryState != null)
        {
            inventoryState.InventoryChanged += RedrawAll;
        }

        RedrawAll();
    }

    private void OnDisable()
    {
        if (inventoryState != null)
        {
            inventoryState.InventoryChanged -= RedrawAll;
        }

        if (GlobalDataPersistenceManager.instance != null)
        {
            GlobalDataPersistenceManager.instance.SaveGame();
        }
    }

    private void RedrawAll()
    {
        RedrawItems();
        RedrawFamiliars();
    }

    private void RedrawItems()
    {
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
