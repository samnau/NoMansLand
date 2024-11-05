using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField]
    bool triggerEventOnStart = false;
    [SerializeField]
    GameEvent defaultEvent;

    private void Start()
    {
        if(triggerEventOnStart && defaultEvent)
        {
            TriggerGameEvent(defaultEvent);
        }
    }
    public void TriggerGameEvent (GameEvent targetEvent)
    {
        targetEvent?.Invoke();
    }

}
