using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField]
    bool triggerEventOnStart = false;
    [SerializeField]
    GameEvent defaultEvent;
    [SerializeField]
    float eventTriggerDelay = 0f;
    [SerializeField]
    bool isOneTimeEvent = false;
    [SerializeField]
    OneTimeEvent oneTimeEventState;
    bool currentEventFired = false;

    // TEMP
    [SerializeField]
    bool isBrokenPoolEvent = false;
    [SerializeField]
    bool isCastleCourtyardEvent = false;

    protected GameStateManager gameStateManager;
    protected DataPersistanceManager dataPersistanceManager;

    bool brokenPoolDialogPlayed = false;
    bool courtyardDialogPlayed = false;

    private void Start()
    {
        //TODO: find a way to abstract this to less one-off bool flag checks
        gameStateManager = FindObjectOfType<GameStateManager>();
        dataPersistanceManager = FindObjectOfType<DataPersistanceManager>();

        brokenPoolDialogPlayed = gameStateManager.brokenPoolDialogPlayed;
        courtyardDialogPlayed = gameStateManager.courtyardDialogPlayed;

        //if (isOneTimeEvent && oneTimeEventState.eventFired)
        //{
        //    print("one time event?");
        //    return;
        //}
        if (isOneTimeEvent && brokenPoolDialogPlayed && !isCastleCourtyardEvent)
        {
            print("one time event?");
            return;
        }
        if (triggerEventOnStart && defaultEvent)
        {
            //TriggerGameEvent(defaultEvent);
            TriggerTimedGameEvent();
            SetOneTimeEventState();
        }
    }

    void SetOneTimeEventState()
    {
        if (oneTimeEventState != null)
        {
            oneTimeEventState.eventFired = true;
        }

        //TODO: abstract this
        if(!brokenPoolDialogPlayed && isBrokenPoolEvent)
        {
            gameStateManager.brokenPoolDialogPlayed = true;
            dataPersistanceManager?.SaveGame();
        }

        //if (!courtyardDialogPlayed && isCastleCourtyardEvent)
        //{
        //    gameStateManager.courtyardDialogPlayed = true;
        //    dataPersistanceManager?.SaveGame();
        //}
    }
    public void TriggerGameEvent (GameEvent targetEvent)
    {
        targetEvent?.Invoke();
        SetOneTimeEventState();
    }

    IEnumerator TriggerGameEventAfterDelay()
    {
        yield return new WaitForSeconds(eventTriggerDelay);
        defaultEvent?.Invoke();
    }

    public void TriggerTimedGameEvent()
    {
        StartCoroutine(TriggerGameEventAfterDelay());
    }

}
