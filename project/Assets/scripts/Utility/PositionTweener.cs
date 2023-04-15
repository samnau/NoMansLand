using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class PositionTweener : BaseTweener
{
    [SerializeField]
    Vector3 endPosition;
    public Vector3 startPositionStatic;

    private void Start()
    {
        startPositionStatic = transform.position;
    }

    IEnumerator SetPosition()
    {
        Vector3 startPostion = transform.position;
        while (progress < 1)
        {
            transform.position = Vector3.Lerp(startPostion, endPosition, progress);
            progress += (Time.deltaTime * speed);
            if (progress >= 1)
            {
                progress = 1f;
                transform.position = endPosition;
            }
            yield return null;
        }
    }

    public void SetPositionOnce([Optional] Vector3 targetPosition, float targetProgress)
    {
        endPosition = targetPosition;
        transform.position = Vector3.Lerp(startPositionStatic, endPosition, targetProgress);
    }

    public void TriggerPosition([Optional] Vector3 targetPosition, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        endPosition = targetPosition;
        progress = 0;
        StartCoroutine(SetPosition());
    }
}
