using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dayCount;
    public string currentScene;
    public string previousDirection;
    public Vector2 playerPosition;
    public bool gameInProgress;
    public bool gameComplete;
    public bool firstBossBeaten;
    public bool brokenPoolDialogPlayed;
    public bool courtyardDialogPlayed;
    public bool firstBossDialogPlayed;
    public Dictionary<string, bool> playerInventory;

    public GameData()
    {
        this.dayCount = 0;
        this.currentScene = "";
        this.playerPosition = Vector2.zero;
        this.playerInventory = new Dictionary<string, bool>();
        this.previousDirection = "up";
    }
}
