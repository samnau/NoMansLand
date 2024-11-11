using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class UtilityScaleTweener : BaseTweener
{
    [SerializeField]
    float endScale = 1f;
    [SerializeField]
    bool animationBounce = false;
    public bool scaleLooping = false;

    // NOTE: for debugging loop animation code
    //private void Start()
    //{
    //    TriggerScaleLooper(.9f, .125f, 0f);
    //}
    IEnumerator SetScale()
    {
        Vector3 targeScale = new Vector3(endScale, endScale, endScale);
        Vector3 startScale = transform.localScale;
        while (progress < 1)
        {
            if(animationBounce)
            {
                transform.localScale = Vector3.Slerp(startScale, targeScale, progress);
            } else
            {
                transform.localScale = Vector3.Lerp(startScale, targeScale, progress);
            }
            progress += (Time.deltaTime * speed);
            if (progress >= 1)
            {
                progress = 1f;
                transform.localScale = targeScale;
            }
            yield return null;
        }
    }


    IEnumerator TweenScaleByDuration(float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time
        Vector3 targeScale = new Vector3(endScale, endScale, endScale);
        Vector3 startScale = transform.localScale;

        while (elapsed_time <= duration)
        {
            if (animationBounce)
            {
                transform.localScale = Vector3.Slerp(startScale, targeScale, EaseOutQuint(elapsed_time / duration));
            }
            else
            {
                transform.localScale = Vector3.Lerp(startScale, targeScale, EaseOutQuint(elapsed_time / duration));
            }
            yield return null;
            elapsed_time += Time.deltaTime;
        }
        transform.localScale = targeScale;
    }

    public void TriggerScaleLooper(float targetScale, float duration, float loopDelay = .5f)
    {
        StartCoroutine(ScaleLooper(targetScale, duration, loopDelay));
    }

    IEnumerator ScaleLooper(float targetScale, float duration, float loopDelay = .5f)
    {

        float startScale = transform.localScale.x;
        endScale = targetScale;
        TriggerScaleByDuration(endScale, duration);
        yield return new WaitForSeconds(duration);

        endScale = startScale;
        TriggerScaleByDuration(endScale, duration);

        yield return new WaitForSeconds(duration);
        if (!scaleLooping)
        {
            yield break;
        }
        yield return new WaitForSeconds(loopDelay);
        StartCoroutine(ScaleLooper(targetScale, duration, loopDelay));
    }

    public void SetUniformScale(float targetScaleFloat)
    {
        transform.localScale = new Vector3(targetScaleFloat, targetScaleFloat, targetScaleFloat);
    }

    public void TriggerScaleByDuration([Optional] float targetScale, [Optional] float duration)
    {
        if(duration == 0)
        {
            duration = .5f;
        }
        endScale = targetScale;
        StartCoroutine(TweenScaleByDuration(duration));
    }

    public void TriggerScale([Optional] float targetScale, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        endScale = targetScale;
        progress = 0;
        StartCoroutine(SetScale());
    }
}
