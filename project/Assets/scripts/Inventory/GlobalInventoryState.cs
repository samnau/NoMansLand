using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalInventoryState : MonoBehaviour, IGlobalDataPersistence
{
    public event Action InventoryChanged;
    public static event System.Action<string> OnInventoryIdCollected;
    public static event System.Action OnActiveChanged;
    public bool activeItemLimitReached = false;
    public bool activeFamiliarLimitReached = true;

    private List<GlobalGameData.InventoryItem> items = new List<GlobalGameData.InventoryItem>();
    private List<GlobalGameData.Familiar> familiars = new List<GlobalGameData.Familiar>();

    private void OnEnable()
    {
        Gem.OnInventoryIdCollected += HandleInventoryIdCollected;
    }

    private void OnDisable()
    {
        Gem.OnInventoryIdCollected -= HandleInventoryIdCollected;
    }

    public void LoadData(GlobalGameData data)
    {
        if (data == null || data.inventory == null)
        {
            items = new List<GlobalGameData.InventoryItem>();
            familiars = new List<GlobalGameData.Familiar>();
            InventoryChanged?.Invoke();
            return;
        }

        items = data.inventory.items != null
            ? new List<GlobalGameData.InventoryItem>(data.inventory.items)
            : new List<GlobalGameData.InventoryItem>();

        familiars = data.inventory.familiars != null
            ? new List<GlobalGameData.Familiar>(data.inventory.familiars)
            : new List<GlobalGameData.Familiar>();

        InventoryChanged?.Invoke();
    }

    public void SaveData(ref GlobalGameData data)
    {
        if (data == null)
        {
            return;
        }

        if (data.inventory == null)
        {
            data.inventory = new GlobalGameData.Inventory();
        }

        data.inventory.items = items != null
            ? new List<GlobalGameData.InventoryItem>(items)
            : new List<GlobalGameData.InventoryItem>();

        data.inventory.familiars = familiars != null
            ? new List<GlobalGameData.Familiar>(familiars)
            : new List<GlobalGameData.Familiar>();
    }

    public IReadOnlyList<GlobalGameData.InventoryItem> GetCollectedItems()
    {
        return items.Where(i => i != null && i.collected && !i.used).ToList();
    }

    public IReadOnlyList<GlobalGameData.Familiar> GetCollectedFamiliars()
    {
        return familiars.Where(f => f != null && f.collected).ToList();
    }

    public GlobalGameData.InventoryItem GetItemById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        return items.FirstOrDefault(i => i != null && i.id == id);
    }

    public GlobalGameData.Familiar GetFamiliarById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        return familiars.FirstOrDefault(f => f != null && f.id == id);
    }

    public void SetCollected(string id, bool collected)
    {
        if (string.IsNullOrEmpty(id))
        {
            return;
        }

        GlobalGameData.InventoryItem item = items.FirstOrDefault(i => i != null && i.id == id);
        if (item != null)
        {
            if (item.collected != collected)
            {
                item.collected = collected;
                InventoryChanged?.Invoke();
            }
            return;
        }

        GlobalGameData.Familiar familiar = familiars.FirstOrDefault(f => f != null && f.id == id);
        if (familiar != null)
        {
            if (familiar.collected != collected)
            {
                familiar.collected = collected;
                InventoryChanged?.Invoke();
            }
        }
    }

    public void SetActive(string id, bool active)
    {
        if (string.IsNullOrEmpty(id))
        {
            return;
        }
        List<GlobalGameData.InventoryItem> activeItems = items.FindAll(item => item.active == true);
        activeItemLimitReached = activeItems.Count >= 2;
        //print($"inventory state active triggered: {id},{active}");

        GlobalGameData.InventoryItem item = items.FirstOrDefault(i => i != null && i.id == id);
        if (item != null)
        {
            if (item.active != active)
            {
                item.active = active;
                OnActiveChanged?.Invoke();
            }
            if(activeItemLimitReached)
            {
                activeItems[0].active = false;
                OnActiveChanged?.Invoke();
            }
            return;
        }

        GlobalGameData.Familiar familiar = familiars.FirstOrDefault(f => f != null && f.id == id);
        if (familiar != null)
        {
            if (familiar.active != active)
            {
                familiar.active = active;
                OnActiveChanged?.Invoke();
            }
        }
    }

    public void SetUsed(string id, bool used)
    {
        if (string.IsNullOrEmpty(id))
        {
            return;
        }

        GlobalGameData.InventoryItem item = items.FirstOrDefault(i => i != null && i.id == id);
        if (item != null)
        {
            if (item.used != used)
            {
                item.used = used;
                InventoryChanged?.Invoke();
            }
            return;
        }

        GlobalGameData.Familiar familiar = familiars.FirstOrDefault(f => f != null && f.id == id);
        if (familiar != null)
        {
            if (familiar.used != used)
            {
                familiar.used = used;
                InventoryChanged?.Invoke();
            }
        }
    }

    private void HandleInventoryIdCollected(string inventoryId)
    {
        SetCollected(inventoryId, true);
    }
}
