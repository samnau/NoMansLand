using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroPositionManager : MonoBehaviour
{
    Fade_Controller fadeController;
    ScenePosition scenePosition;
    PositionMarker[] positionMarkers;
    // Start is called before the first frame update
    void Start()
    {
        fadeController = GameObject.FindObjectOfType<Fade_Controller>();
        scenePosition = fadeController.scenePosition;
        string currentDirection = scenePosition.currentDirection;
        string inputDirection = currentDirection == "up" || currentDirection == "down" ? scenePosition.lastDirection : currentDirection;
        InputStateTracker inputStateTracker = gameObject.GetComponent<InputStateTracker>();
        positionMarkers = GameObject.FindObjectsOfType<PositionMarker>();
        print($"there are this many markers: {positionMarkers.Length}");
        PositionMarker targetMarker = positionMarkers.FirstOrDefault(positionMarker => positionMarker.direction.ToString() == currentDirection);

        if(targetMarker)
        {
            //print($"target pos: {targetMarker.transform.position.x},{targetMarker.transform.position.y}");
            print($"Molly faces: {inputDirection}");
            this.transform.position = targetMarker.transform.position;
            inputStateTracker.direction = inputDirection;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
