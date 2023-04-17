using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class UtilityScaleTweener : BaseTweener
{
    [SerializeField]
    float endScale;
    [SerializeField]
    bool animationBounce = false;
    private void Start()
    {
        TriggerScale();
    }
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
