using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : SceneButton
{
    [SerializeField]
    ScriptableObject[] objectsToReset;
    ScenePosition scenePosition;
    [SerializeField]
    GameEvent confirmationRequired;

    protected override void ButtonInit()
    {
        base.ButtonInit();
        button.onClick.AddListener(StartClickHandler);
    }

    void StartClickHandler()
    {
        if (confirmationRequired && gameStateData != null && gameStateData.gameInProgress)
        {
            confirmationRequired.Invoke();
            return;
        }
        ResetGameState();
        ChangeScene("BrokenPool");
    }
    public void ResetGameState()
    {
        if(objectsToReset.Length == 0)
        {
            return;
        }

        List<OneTimeEvent> oneTimeEvents = new List<OneTimeEvent>();
        //TODO: add code that enables the continue button. will have to be stored in object or elsewhere.


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
            scenePosition.currentDirection = "up";
            scenePosition.lastDirection = "down";
        }

        if(gameStateData != null)
        {
            gameStateData.gameInProgress = true;
            gameStateData.gameComplete = false;
        }
    }

}
