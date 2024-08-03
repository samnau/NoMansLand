using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePositionManager : MonoBehaviour
{
    string[] directionValues = { "left", "right", "up", "down" };
    enum Directions
    { up, down, left, right }
    public string targetDirection = "down";
    public string previousDirection = "up";
    Dictionary<string, string> startDirection =
    new Dictionary<string, string>();
    public GameObject hero;
    public GameObject targetPosition;

    private void Awake()
    {
        GameObject[] scenPositionManagers = GameObject.FindGameObjectsWithTag("ScenePosition");
        startDirection.Add("left", "right");
        startDirection.Add("right", "left");
        startDirection.Add("up", "down");
        startDirection.Add("down", "up");
    }
    string GetStartDirection(string direction)
    {
        return startDirection[direction];
    }

    public void SetStartDirection()
    {
       targetDirection = GetStartDirection(previousDirection);
    }
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        //print($"direction: {targetDirection}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
