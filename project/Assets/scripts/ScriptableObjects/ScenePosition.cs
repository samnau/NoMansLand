using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ScenePosition", menuName ="Scene Position")]
public class ScenePosition : ScriptableObject
{
    public string lastDirection = "up";
    public string currentDirection = "down";
    public Dictionary<string, string> startDirection =
    new Dictionary<string, string>{
            { "left", "right" },
            { "right", "left" },
            { "up", "down" },
            { "down", "up" },
        };
}
