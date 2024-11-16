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

    private void Start()
    {
        if(triggerEventOnStart && defaultEvent)
        {
            //TriggerGameEvent(defaultEvent);
            TriggerTimedGameEvent();
        }
    }
    public void TriggerGameEvent (GameEvent targetEvent)
    {
        targetEvent?.Invoke();
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
