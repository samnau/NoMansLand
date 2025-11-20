using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour, IGlobalDataPersistence
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
    bool oneTimeEventFired = false;

    enum OneTimeEvents {
        test,
        castleCourtyard,
        castleThroneRoom
    };
    [SerializeField]
    OneTimeEvents oneTimeEventName;

    private void Start()
    {

        if(isOneTimeEvent && oneTimeEventFired)
        {
            Debug.LogWarning("one time event has been fired already");
            return;
        }
        if (triggerEventOnStart && defaultEvent)
        {
            print("trigger event on start");
            TriggerTimedGameEvent();
        }
    }

    //TODO: remove this after final testing
    public void TestOneTimeEvent()
    {
        print($"one time event fired from test function");
    }

    void SetOneTimeEventState()
    {

        if (isOneTimeEvent && !oneTimeEventFired)
        {
            oneTimeEventFired = true;
            Debug.Log($"flag one time event as true for {GetOneTimeEventName()}");
        }

    }

    string GetOneTimeEventName()
    {
        return oneTimeEventName.ToString();
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

    public void LoadData(GlobalGameData data)
    {
        FieldInfo fieldInfo = data.oneTimeEvents.GetType().GetField(GetOneTimeEventName());
        oneTimeEventFired = (bool)fieldInfo.GetValue(data.oneTimeEvents);
    }

    public void SaveData(ref GlobalGameData data)
    {
        FieldInfo fieldInfo = data.oneTimeEvents.GetType().GetField(GetOneTimeEventName());
        if(isOneTimeEvent)
        {
            fieldInfo.SetValue(data.oneTimeEvents, oneTimeEventFired);
        }
    }

}
