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
        // NOTE: for debugging position setting
        //print(targetPosition);
        Vector3 startPostion = transform.position;
        while (elapsed_time < duration)
        {
            transform.position = Vector3.Lerp(startPostion, targetPosition, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }
        transform.localPosition = targetPosition;
    }

    IEnumerator SetLocalPositionByDuration(Vector3 targetPosition, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time
        Vector3 startPostion = transform.localPosition;
        //print($"local pos: {startPostion}");
        //print($"elapsed: {elapsed_time}");
        //print($"duration: {duration}");
        while (elapsed_time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPostion, targetPosition, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }

        transform.localPosition = targetPosition;
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

    public void TriggerEndPositionSimple()
    {
        print("simple position tween trigger");
        progress = 0;
        StartCoroutine(SetLocalPositionByDuration(endPosition, speed));
    }

    public bool IsUiVisible(GameObject targetObject)
    {
        if (targetObject is null)
        {
            targetObject = gameObject;
        }
        //if (targetObject == null || targetObject.transform.parent == null)
        //{
        //    return false;
        //}

        Camera camera = Camera.main;
        if (camera == null)
        {
            return false;
        }

        if (targetObject.activeSelf)
        {
            RectTransform rectTransform = targetObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector3[] corners = new Vector3[4];
                rectTransform.GetWorldCorners(corners);

                foreach (Vector3 corner in corners)
                {
                    Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, corner);
                    if (screenPoint.x >= 0 && screenPoint.x <= Screen.width &&
                        screenPoint.y >= 0 && screenPoint.y <= Screen.height)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public enum ToggleDirection { vertical, horizontal };

    public void ToggleUi(ToggleDirection direction, GameObject targetObject)
    {
        bool isVisible = IsUiVisible(targetObject);

        switch (direction)
        {
            case ToggleDirection.horizontal:
                if (isVisible)
                {
                    MoveUIBackward(0.5f);
                }
                else
                {
                    MoveUIForward(0.5f);
                }
                break;
            case ToggleDirection.vertical:
                if (isVisible)
                {
                    MoveUIUpward(0.5f);
                }
                else
                {
                    MoveUIDownward(0.5f);
                }
                break;
        }
    }

    public void MoveUIForward(float duration)
    {
        MoveUIByWidth(duration, true);
    }

    public void MoveUIBackward(float duration)
    {
        MoveUIByWidth(duration, false);
    }

    public void MoveUIByWidth(float duration, bool forward = true)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("No RectTransform found on this UI object");
            return;
        }

        // Get the width from RectTransform rect, accounting for local scale
        float objectWidth = rectTransform.rect.width * rectTransform.localScale.x;

        float widthOffset = forward ? objectWidth : -objectWidth;

        // Calculate target position (move by object width in local space)
        Vector3 currentPosition = rectTransform.localPosition;
        Vector3 targetPosition = new Vector3(
            currentPosition.x + widthOffset,
            currentPosition.y,
            currentPosition.z
        );

        // Use local position tweening for UI objects
        TriggerLocalPositionByDuration(targetPosition, duration);
    }

    public void MoveUIUpward(float duration)
    {
        MoveUIByHeight(duration, true);
    }

    public void MoveUIDownward(float duration)
    {
        MoveUIByHeight(duration, false);
    }

    public void MoveUIByHeight(float duration, bool forward = true)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("No RectTransform found on this UI object");
            return;
        }

        // Get the height from RectTransform rect, accounting for local scale
        float objectHeight = rectTransform.rect.height * rectTransform.localScale.y;

        float heightOffset = forward ? objectHeight : -objectHeight;

        // Calculate target position (move by object height in local space)
        Vector3 currentPosition = rectTransform.localPosition;
        Vector3 targetPosition = new Vector3(
            currentPosition.x,
            currentPosition.y + heightOffset,
            currentPosition.z
        );

        // Use local position tweening for UI objects
        TriggerLocalPositionByDuration(targetPosition, duration);
    }

    public void TriggerLocalPositionByDuration([Optional] Vector3 targetPosition, [Optional] float duration)
    {
        StartCoroutine(SetLocalPositionByDuration(targetPosition, duration));
    }

    public void TriggerPositionByDuration([Optional] Vector3 targetPosition, [Optional] float duration)
    {
        StartCoroutine(SetPositionByDuration(targetPosition, duration));
    }
}
