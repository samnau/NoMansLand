using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTweener : BaseTweener
{
    public bool scaleMeUp = false;
    public bool scaleMeDown = true;
    Transform targetTransform;
    public float targetScaleFloat = 1.5f;
    Vector3 targetScale;
    Vector3 initialScale;
    Vector3 currentScale;
    float changeIncrement = 0;
   // public float speed = 0.25f;

	// Use this for initialization
	void Start () {
        targetTransform = gameObject.transform;
        initialScale = targetTransform.localScale;
        targetScale = initialScale + new Vector3(targetScaleFloat, targetScaleFloat, targetScaleFloat);
	}
    void findCurrentScale()
    {
        currentScale = targetTransform.localScale;
    }

    //NOTE: look at removing this method
	void ScaleUp ()
    {
        changeIncrement += (Time.deltaTime * speed);
        findCurrentScale();
        var currentScaleTarget = scaleMeUp ? targetScale : initialScale;
        if (changeIncrement < 1.0f)
        {
            transform.localScale = Vector3.Lerp(currentScale, currentScaleTarget, changeIncrement);
        }
    }

    //NOTE: look at removing this method

    public void ResetIncrement()
    {
        changeIncrement = 0;
    }

    //NOTE: look at removing this method

    void ScaleDown()
    {
        changeIncrement += (Time.deltaTime * speed);
        findCurrentScale();
        if (changeIncrement < 1.0f)
        {
            transform.localScale = Vector3.Lerp(currentScale, initialScale, changeIncrement);
        }
        else
        {
            scaleMeDown = false;
            changeIncrement = 0;
        }
    }

    IEnumerator SetScaleByDuration(float targetScaleFloat, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration);
        Vector3 targetLcoalScale = new Vector3(targetScaleFloat, targetScaleFloat, targetScaleFloat);
        Vector3 initalScale = transform.localScale;
        while (elapsed_time < duration)
        {
            transform.localScale = Vector3.Lerp(initalScale, targetLcoalScale, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }

    public void SetUniformScale(float targetScaleFloat)
    {
        transform.localScale = new Vector3(targetScaleFloat, targetScaleFloat, targetScaleFloat);
    }

    public void TriggerUniformScaleTween(float targetScale, float duration)
    {
        StartCoroutine(SetScaleByDuration(targetScale, duration));
    }
    //NOTE: look at removing this update, this original code may not have ever been put into use
    void Update () {
        //ScaleUp();

    }
}
