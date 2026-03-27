using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryVisualDatabase", menuName = "Inventory/Visual Database")]
public class InventoryVisualDatabase : ScriptableObject
{
    [System.Serializable]
    public class IconEntry
    {
        public string id;
        public Sprite sprite;
    }

    [Header("Items")]
    [SerializeField] private List<IconEntry> items = new List<IconEntry>();

    [Header("Familiars")]
    [SerializeField] private List<IconEntry> familiars = new List<IconEntry>();

    private Dictionary<string, Sprite> itemIconsById;
    private Dictionary<string, Sprite> familiarIconsById;

    private void OnEnable()
    {
        BuildDictionaries();
    }

    private void BuildDictionaries()
    {
        itemIconsById = new Dictionary<string, Sprite>();
        foreach (var entry in items)
        {
            if (!string.IsNullOrEmpty(entry.id) && entry.sprite != null)
            {
                itemIconsById[entry.id] = entry.sprite;
            }
        }

        familiarIconsById = new Dictionary<string, Sprite>();
        foreach (var entry in familiars)
        {
            if (!string.IsNullOrEmpty(entry.id) && entry.sprite != null)
            {
                familiarIconsById[entry.id] = entry.sprite;
            }
        }
    }

    public Sprite GetItemIcon(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        return itemIconsById != null && itemIconsById.TryGetValue(id, out Sprite sprite) ? sprite : null;
    }

    public Sprite GetFamiliarIcon(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        return familiarIconsById != null && familiarIconsById.TryGetValue(id, out Sprite sprite) ? sprite : null;
    }
}
