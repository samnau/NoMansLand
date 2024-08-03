using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveDataManager : MonoBehaviour
{
   // [HideInInspector] public string saveDataFilePath = "/ufSaveData.gd";
   // [HideInInspector] public string preferencesDataFilePath = "/ufPreferencesData.gd";
    [HideInInspector] public bool newGame;
    [HideInInspector] public string sceneName;
    [HideInInspector] public int days;
    [HideInInspector] public string sceneDirection;

    public void PlayerData (PlayerData playerData)
    {
        newGame = playerData.newGame;
        sceneName = playerData.sceneName;
        days = playerData.days;
        sceneDirection = playerData.sceneDirection;
    }

}
