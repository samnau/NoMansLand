using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastVisitedScene", menuName = "Last Visited Scene")]

public class LastVisitedScene : ScriptableObject
{
    string defaultScene = "BrokenPool";
    public string lastScene;
    public bool reset = false;
    void Awake()
    {
        if(lastScene == null || reset)
        {
            lastScene = defaultScene;
        }
    }

}
