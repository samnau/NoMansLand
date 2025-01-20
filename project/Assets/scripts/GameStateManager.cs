using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IDataPersistance
{
    private int dayCount = 0;
    [HideInInspector] public string targetSceneName = "";
    [HideInInspector] public bool gameInProgress = false;
    [HideInInspector] public bool gameComplete = false;
    [HideInInspector] public bool brokenPoolDialogPlayed = false;
    [HideInInspector] public bool courtyardDialogPlayed = false;

    public void LoadData(GameData data)
    {
        this.dayCount = data.dayCount;
        this.targetSceneName = data.currentScene;
        this.gameInProgress = data.gameInProgress;
        this.gameComplete = data.gameComplete;
        this.brokenPoolDialogPlayed = data.brokenPoolDialogPlayed;
        this.courtyardDialogPlayed = data.courtyardDialogPlayed;
    }

    public void SaveData(ref GameData data)
    {
        data.dayCount = this.dayCount;
        data.currentScene = this.targetSceneName;
        data.gameInProgress = this.gameInProgress;
        data.gameComplete = this.gameComplete;
        data.brokenPoolDialogPlayed = this.brokenPoolDialogPlayed;
        data.courtyardDialogPlayed = this.courtyardDialogPlayed;
    }

    void IncreaseDayCount()
    {
        dayCount++;
    }

    public void SaveSceneName(ref GameData data)
    {
        data.currentScene = this.targetSceneName;
    }

}
