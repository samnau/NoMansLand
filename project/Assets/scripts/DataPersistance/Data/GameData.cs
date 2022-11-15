using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dayCount;
    public string currentScene;
    public Vector2 playerPosition;
    public Dictionary<string, bool> playerInventory;

    public GameData()
    {
        this.dayCount = 0;
        this.currentScene = "";
        this.playerPosition = Vector2.zero;
        this.playerInventory = new Dictionary<string, bool>();
    }
}
