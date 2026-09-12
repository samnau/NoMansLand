using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
    public static event HandleGemCollected OnGemCollected;
    public delegate void HandleGemCollected(ItemData itemData);
    public ItemData gemData;
    public static event System.Action<string> OnInventoryIdCollected;
    [SerializeField] private string inventoryId;
//    void Add(ItemData itemData)
    public void Collect()
    {
        print("Ooooh a gem!");
        Destroy(gameObject);
        OnGemCollected?.Invoke(gemData);
        if (!string.IsNullOrEmpty(inventoryId))
        {
            OnInventoryIdCollected?.Invoke(inventoryId);
        }
    }
}
