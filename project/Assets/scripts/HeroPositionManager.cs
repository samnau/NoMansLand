using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroPositionManager : MonoBehaviour
{
    Fade_Controller fadeController;
    ScenePosition scenePosition;
    PositionMarker[] positionMarkers;
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
}
