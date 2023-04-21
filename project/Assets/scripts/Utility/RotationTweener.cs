using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

public class RotationTweener : BaseTweener
{
    [SerializeField]
    float endRotation;
    [SerializeField]
    bool continuousSpinActive = false;
    [SerializeField]
    public enum targetDirections {Left, Right};
    [SerializeField]
    targetDirections selectedDirection;
    
    IEnumerator SetRotation()
    {
        Quaternion targetAngles = Quaternion.Euler(0, 0, endRotation);
        Quaternion startAngles = transform.rotation;
       while(progress < 1)
        {
            transform.rotation = Quaternion.Slerp(startAngles, targetAngles, Mathf.SmoothStep(0.0f, 1.0f, progress));
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
        float directionModifier = selectedDirection == targetDirections.Left ? 1f : -1f;
        float targetSpeed = speed * directionModifier;
        transform.Rotate(new Vector3(0f, 0f, targetSpeed * Time.fixedDeltaTime), Space.Self);
        yield return new WaitForFixedUpdate();
        StartCoroutine(RotateContinuous());
    }
   
    public void TriggerContinuousRotation([Optional]float targetSpeed)
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
    private void Start()
    {
        if (continuousSpinActive)
        {
            TriggerContinuousRotation();
        }
    }
}
