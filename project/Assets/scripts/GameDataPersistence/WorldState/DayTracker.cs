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
        dayCount = int.Parse(data.gameState["dayCount"]);
        print(data.gameState["dayCount"]);
        print($"saved settings have fx volume: {data.gameSettings.fxVolume}");
    }

    public void SaveData(ref GlobalGameData data)
    {
        data.gameState["dayCount"] = this.dayCount.ToString();
        data.worldState.dayCount = this.dayCount;
    }
}
