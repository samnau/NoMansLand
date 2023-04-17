using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class RotationTweener : BaseTweener
{
    [SerializeField]
    float endRotation;

    IEnumerator SetRotation()
    {
        Quaternion targetAngles = Quaternion.Euler(0, 0, endRotation);
        Quaternion startAngles = transform.rotation;
       while(progress < 1)
        {
            transform.rotation = Quaternion.Slerp(startAngles, targetAngles, progress);
            progress += (Time.deltaTime * speed);
            if(progress >= 1)
            {
                progress = 1f;
                transform.rotation = targetAngles;
            }
            yield return null;
        } 
    }

    IEnumerator RotateContinuous()
    {
        transform.Rotate(new Vector3(0f, 0f, speed * Time.fixedDeltaTime), Space.Self);
        yield return new WaitForFixedUpdate();
        StartCoroutine(RotateContinuous());
    }
   
    public void TriggerContinuousRotation(float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        StartCoroutine(RotateContinuous());
    }

    void StopContinuousRotation()
    {
        StopCoroutine(RotateContinuous());
    }
    public void TriggerRotation([Optional] float targetRotation, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        endRotation = targetRotation;
        progress = 0;
        StartCoroutine(SetRotation());
    }
}
