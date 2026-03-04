using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTweener : BaseTweener
{
    [SerializeField]
    Vector3 beginningPosition;
    [SerializeField]
    Vector3 endPosition;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //var positionCount = new Vector3[lineRenderer.positionCount];
        //var startPositions = lineRenderer.GetPositions(positionCount);
        //print($"line positions {startPositions}");
        //Vector3 testPositon = new Vector3(4, 0, 0);
        ////SetLineEndPosition(testPositon);
        //Vector3 testEndPos = new Vector3(1.78f, -3.19f, 0);
        //StartCoroutine(TestTargetPos(testEndPos));
        SetLineEndPosition(beginningPosition);
    }

    public void TriggerLineTweenStart()
    {
        StartCoroutine(SetLineEndPositonByDuration(endPosition, speed));
    }

    public void TriggerLineTweenEnd()
    {
        StartCoroutine(SetLineBeginningPositonByDuration(endPosition, speed));
    }

    void SetLineEndPosition(Vector3 targetPosition)
    {
        if(lineRenderer is null)
        {
            return;
        }

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, targetPosition);
    }

    void SetLineBeginningPosition(Vector3 targetPosition)
    {
        if (lineRenderer is null)
        {
            return;
        }

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, targetPosition);
    }

    IEnumerator TestTargetPos(Vector3 testPos)
    {
        yield return new WaitForSeconds(3f);
        //SetLineEndPosition(testPos);
        StartCoroutine(SetLineEndPositonByDuration(testPos, speed));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SetLineBeginningPositonByDuration(testPos, speed));
    }

    IEnumerator SetLineEndPositonByDuration(Vector3 targetPosition, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration);
        var startPosition = lineRenderer.GetPosition(0);
        while (elapsed_time < duration)
        {
            var newPositon = Vector3.Lerp(startPosition, targetPosition, EaseInOutQuad(elapsed_time / duration));
            SetLineEndPosition(newPositon);
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }

    IEnumerator SetLineBeginningPositonByDuration(Vector3 targetPosition, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration);
        var startPosition = lineRenderer.GetPosition(0);
        while (elapsed_time < duration)
        {
            var newPositon = Vector3.Lerp(startPosition, targetPosition, EaseInOutQuad(elapsed_time / duration));
            SetLineBeginningPosition(newPositon);
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
