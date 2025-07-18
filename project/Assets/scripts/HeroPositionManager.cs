using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class HeroPositionManager : MonoBehaviour
{
    Fade_Controller fadeController;
    ScenePosition scenePosition;
    PositionMarker[] positionMarkers;
    public Vector3 position;
    public string direction;

    string lastDirection = "up";
    string currentDirection = "down";
    Dictionary<string, string> startDirectionDictionary =
    new Dictionary<string, string>{
            { "left", "right" },
            { "right", "left" },
            { "up", "down" },
            { "down", "up" },
        };

    // NOTE: this file sets both the direction to face and the player position from the last direction recieved
    // for vertical direction it takes the last direction set from the previous scene for facing direction
    // for horizontal direction, it just takes the last direction for facing direction
    // current direction gets set off of the opposite direction of last direction
    // last direction is pulled from the save data via the fade controller's tracking of that direction value
    void Start()
    {
        fadeController = GameObject.FindObjectOfType<Fade_Controller>();
        if(fadeController == null)
        {
            return;
        }

        string previousDirection = fadeController.lastDirection;
        string currentDirection = startDirectionDictionary[previousDirection];
        string inputDirection = currentDirection == "up" || currentDirection == "down" ? previousDirection : currentDirection;
        InputStateTracker inputStateTracker = gameObject.GetComponent<InputStateTracker>();
        positionMarkers = GameObject.FindObjectsOfType<PositionMarker>();
        PositionMarker targetMarker = positionMarkers.FirstOrDefault(positionMarker => positionMarker.direction.ToString() == currentDirection);

        if(targetMarker)
        {
            this.transform.position = targetMarker.transform.position;
            inputStateTracker.direction = inputDirection;
        }
    }

}
