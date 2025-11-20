using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [HideInInspector] public bool newGame = true;
    [HideInInspector] public string sceneName = "";
    [HideInInspector] public int days = 0;
    [HideInInspector] public string sceneDirection = "";
    [HideInInspector] public bool gameInProgress = false;
    [HideInInspector] public bool gameComplete = false;
    [HideInInspector] public bool firstBossBeaten = false;
    [HideInInspector] public bool brokenPoolDialogPlayed = false;
    [HideInInspector] public bool courtyardDialogPlayed = false;
    [HideInInspector] public bool firstBossDialogPlayed = false;

}
