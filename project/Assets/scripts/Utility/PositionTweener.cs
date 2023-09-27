using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class PositionTweener : BaseTweener
{
    public bool cancelTween = false;
    [SerializeField]
    Vector3 endPosition;
    Vector3 startPositionStatic;

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
            //            progress += (Time.deltaTime * speed);
            progress += EaseInQuad((Time.deltaTime * speed));

            if (progress >= 1)
            {
                progress = 1f;
                transform.position = endPosition;
            }
            yield return null;
        }
    }

    IEnumerator YoYo(Vector3 targetPosition, float duration)
    {
        Vector3 originalPosition = this.transform.position;
        float elapsed_time = Mathf.Clamp(0, 0, duration);
        StartCoroutine(SetPositionByDuration(targetPosition, duration));
        while (elapsed_time < duration && !cancelTween)
        {
            yield return null;
            elapsed_time += Time.deltaTime;
        }
        elapsed_time = 0;
        StartCoroutine(SetPositionByDuration(originalPosition, duration));
        while (elapsed_time < duration && !cancelTween)
        {
            yield return null;
            elapsed_time += Time.deltaTime;
        }
        elapsed_time = 0;
        StartCoroutine(YoYo(targetPosition, duration));
    }

    IEnumerator SetPositionByDuration(Vector3 targetPosition, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time

        Vector3 startPostion = transform.position;
        while (elapsed_time < duration)
        {
            transform.position = Vector3.Lerp(startPostion, targetPosition, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }

    //public static IEnumerator ChangeObjectPos(Transform transform, float y_target, float duration)
    //{
    //    float elapsed_time = 0; //Elapsed time

    //    Vector3 pos = transform.position; //Start object's position

    //    float y_start = pos.y; //Start "y" value

    //    while (elapsed_time <= duration) //Inside the loop until the time expires
    //    {
    //        pos.y = Mathf.Lerp(y_start, y_target, elapsed_time / duration); //Changes and interpolates the position's "y" value

    //        transform.position = pos;//Changes the object's position

    //        yield return null; //Waits/skips one frame

    //        elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
    //    }
    //}

    public void StartYoYo(Vector3 targetPosition, float duration)
    {
        StartCoroutine(YoYo(targetPosition, duration));
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

    public void TriggerPositionByDuration([Optional] Vector3 targetPosition, [Optional] float duration)
    {
        StartCoroutine(SetPositionByDuration(targetPosition, duration));
    }
}
