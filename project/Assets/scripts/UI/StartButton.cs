using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : SceneButton, IGlobalDataPersistence
{
    [SerializeField]
    ScriptableObject[] objectsToReset;
    ScenePosition scenePosition;
    [SerializeField]
    GameEvent confirmationRequired;
    bool gameInProgress = false;

    protected override void ButtonInit()
    {
        base.ButtonInit();
        button.onClick.AddListener(StartClickHandler);
    }

    void StartClickHandler()
    {
        //if (confirmationRequired && prefManager != null && prefManager.GetInProgressState() == 1)
        //{
        //    confirmationRequired.Invoke();
        //    return;
        //}

        if (confirmationRequired && gameInProgress)
        {
            confirmationRequired.Invoke();
            return;
        }
        ResetGameState();

        //Load the intro scene
        ChangeScene("Intro");
    }

    public void LoadData(GlobalGameData data)
    {
        gameInProgress = data.worldState.gameInProgress;
    }

    public void SaveData(ref GlobalGameData data)
    {
        data.worldState.gameInProgress = gameInProgress;
    }
    public void ResetGameState()
    {
        if(objectsToReset.Length == 0)
        {
            return;
        }

        gameInProgress = true;

        //NOTE: this one time event code will be refactored when I create the save system
        List<OneTimeEvent> oneTimeEvents = new List<OneTimeEvent>();
        //TODO: add code that enables the continue button. will have to be stored in object or elsewhere.

        for (int i = 0; i < objectsToReset.Length; i++)
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

        if(prefManager != null)
        {
            prefManager.SetBrokenPoolState(0);
            prefManager.SetCastleCourtyardState(0);
            prefManager.SetBonusState(0);
            prefManager.SetGameInProgress(1);
        }
    }

}
