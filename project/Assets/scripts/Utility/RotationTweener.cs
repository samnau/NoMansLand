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
