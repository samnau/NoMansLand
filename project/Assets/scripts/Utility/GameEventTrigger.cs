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

    PlayerPrefManager prefManager;

    bool brokenPoolDialogPlayed = false;
    bool courtyardDialogPlayed = false;

    private void Start()
    {
        //TODO: find a way to abstract this to less one-off bool flag checks
        prefManager = FindObjectOfType<PlayerPrefManager>();
        SetDialogState();

        if (isOneTimeEvent && brokenPoolDialogPlayed && !isCastleCourtyardEvent)
        {
            print("one time event has been fired");
            return;
        }
        if (triggerEventOnStart && defaultEvent)
        {
            TriggerTimedGameEvent();
        }
    }

    void SetDialogState()
    {
        brokenPoolDialogPlayed = prefManager.GetBrokenPoolState() == 1;
    }

    void SetOneTimeEventState()
    {
        if (oneTimeEventState != null)
        {
            oneTimeEventState.eventFired = true;

        }


        //TODO: abstract this
        if (!brokenPoolDialogPlayed && isBrokenPoolEvent)
        {
            prefManager.SetBrokenPoolState(1);
        }
    }
    public void TriggerGameEvent (GameEvent targetEvent)
    {
        targetEvent?.Invoke();
        SetOneTimeEventState();
    }

    IEnumerator TriggerGameEventAfterDelay()
    {
        yield return new WaitForSeconds(eventTriggerDelay);
        SetOneTimeEventState();
        defaultEvent?.Invoke();
    }

    public void TriggerTimedGameEvent()
    {
        StartCoroutine(TriggerGameEventAfterDelay());
    }

}
