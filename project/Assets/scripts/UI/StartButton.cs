using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : SceneButton
{
    [SerializeField]
    ScriptableObject[] objectsToReset;
    ScenePosition scenePosition;

    // Start is called before the first frame update
    void Start()
    {
        fadeController = FindObjectOfType<Fade_Controller>();
    }

    public void ResetGameState()
    {
        if(objectsToReset.Length == 0)
        {
            return;
        }

        List<OneTimeEvent> oneTimeEvents = new List<OneTimeEvent>();


        for(int i = 0; i < objectsToReset.Length; i++)
        {
            if(objectsToReset[i].GetType() == typeof(OneTimeEvent))
            {
                oneTimeEvents.Add(objectsToReset[i] as OneTimeEvent);
            }

            if (objectsToReset[i].GetType() == typeof(ScenePosition))
            {
                scenePosition = objectsToReset[i] as ScenePosition;
            }
        }

        foreach(OneTimeEvent oneTimeEvent in oneTimeEvents)
        {
            oneTimeEvent.eventFired = false;
        }

        if(scenePosition != null)
        {
            print("scene pos found");
            scenePosition.currentDirection = "up";
            scenePosition.lastDirection = "down";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
