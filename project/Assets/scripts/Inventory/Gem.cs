using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
    public static event HandleGemCollected OnGemCollected;
    public delegate void HandleGemCollected(ItemData itemData);
    public ItemData gemData;
//    void Add(ItemData itemData)
    public void Collect()
    {
        print("Ooooh a gem!");
        Destroy(gameObject);
        OnGemCollected.Invoke(gemData);
    }
}
