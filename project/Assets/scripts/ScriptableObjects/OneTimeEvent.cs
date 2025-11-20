using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OneTimeEvent", menuName = "One Time Event")]
public class OneTimeEvent : ScriptableObject
{
    public bool eventFired = false;
}
