using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventorySlot;
    static int inventoryMax = 21;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(inventoryMax);
    public List<InventoryItem> defaultInventory = new List<InventoryItem>(inventoryMax);

    private void OnEnable()
    {
        Inventory.OnInventoryChange += DrawInventory;
    }
    private void OnDisable()
    {
        Inventory.OnInventoryChange -= DrawInventory;
    }
    // Start is called before the first frame update
    private void ResetInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlot>(19);
    }
    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        for(int i = 0; i < inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }
        for (int i = 0; i < inventory.Count; i++) {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(inventorySlot);
        newSlot.transform.SetParent(transform, false);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        inventorySlots.Add(newSlotComponent);
    }
    void Start()
    {
        DrawInventory(defaultInventory);
    }
}
