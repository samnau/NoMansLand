using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    [SerializeField] GameEvent shakeCamera;

    public void TriggerCameraShake()
    {
        shakeCamera.Invoke();
    }
}
