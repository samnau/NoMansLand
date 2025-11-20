using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game State")]
public class GameState : ScriptableObject
{
    public bool gameInProgress = false;
    public bool gameComplete = false;
}
