using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class HeroPositionManager : MonoBehaviour, ISaveable
{
    Fade_Controller fadeController;
    ScenePosition scenePosition;
    PositionMarker[] positionMarkers;
    public Vector3 position;
    public string direction;
    void Start()
    {
        fadeController = GameObject.FindObjectOfType<Fade_Controller>();
        if(fadeController == null)
        {
            return;
        }
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
    public object SaveState()
    {
        return new SaveData()
        {
            position = this.position,
            direction = this.direction
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        position = saveData.position;
        direction = saveData.direction;
    }
    [Serializable]
    private struct SaveData
    {
        public Vector3 position;
        public string direction;
    }
}
