using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTracker : MonoBehaviour, IGlobalDataPersistence
{
    int dayCount = 0;

    public void AddDays()
    {
        this.dayCount++;
    }

    public void ResetDays()
    {
        this.dayCount = 0;
    }

    public void LoadData(GlobalGameData data)
    {
        dayCount = data.worldState.dayCount;
        //print(data.gameState["dayCount"]);
        print($"saved settings have inventory, collected: {data.inventory.items[1].collected}");
    }

    public void SaveData(ref GlobalGameData data)
    {
        //data.gameState["dayCount"] = this.dayCount.ToString();
        data.worldState.dayCount = this.dayCount;
    }
}
