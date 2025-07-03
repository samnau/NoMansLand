using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class HeroPositionManager : MonoBehaviour, IGlobalDataPersistence
{
    Fade_Controller fadeController;
    ScenePosition scenePosition;
    PositionMarker[] positionMarkers;
    public Vector3 position;
    public string direction;

    string lastDirection = "up";
    string currentDirection = "down";
    Dictionary<string, string> startDirection =
    new Dictionary<string, string>{
            { "left", "right" },
            { "right", "left" },
            { "up", "down" },
            { "down", "up" },
        };

    void Start()
    {
        fadeController = GameObject.FindObjectOfType<Fade_Controller>();
        if(fadeController == null)
        {
            return;
        }
        // TODO: work on converting all of this scene position based code to save data based code
        scenePosition = fadeController?.scenePosition;
        string currentDirection = scenePosition?.currentDirection;
        string inputDirection = currentDirection == "up" || currentDirection == "down" ? scenePosition.lastDirection : currentDirection;
        InputStateTracker inputStateTracker = gameObject.GetComponent<InputStateTracker>();
        positionMarkers = GameObject.FindObjectsOfType<PositionMarker>();
        PositionMarker targetMarker = positionMarkers.FirstOrDefault(positionMarker => positionMarker.direction.ToString() == currentDirection);

        if(targetMarker)
        {
            this.transform.position = targetMarker.transform.position;
            inputStateTracker.direction = inputDirection;
        }
    }
    //public object SaveState()
    //{
    //    return new SaveData()
    //    {
    //        position = this.position,
    //        direction = this.direction
    //    };
    //}

    //public void LoadState(object state)
    //{
    //    var saveData = (SaveData)state;
    //    position = saveData.position;
    //    direction = saveData.direction;
    //}
    //[Serializable]
    //private struct SaveData
    //{
    //    public Vector3 position;
    //    public string direction;
    //}

    public void LoadData(GlobalGameData data)
    {
        // this is wrong because the loaded direction needs to be opposite of the last direction
        // also doesn't need to always be from save data
        // this shouldn't be setting direction. that should only happen in fade triggers
        currentDirection = data.worldState.lastDirection;
        //print(data.gameState["dayCount"]);
        print($"loading last direction from save for hero: {data.worldState.lastDirection}");
    }

    public void SaveData(ref GlobalGameData data)
    {
        //data.gameState["dayCount"] = this.dayCount.ToString();
        data.worldState.lastDirection = this.currentDirection;
    }
}
