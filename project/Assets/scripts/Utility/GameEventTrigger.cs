using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{

    public void TriggerGameEvent (GameEvent targetEvent)
    {
        targetEvent?.Invoke();
    }

}
