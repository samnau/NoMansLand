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

    private void Start()
    {
        if(isOneTimeEvent && oneTimeEventState.eventFired)
        {
            print("one time event?");
            return;
        }
        if(triggerEventOnStart && defaultEvent)
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
