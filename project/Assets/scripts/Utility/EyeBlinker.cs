using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinker : MonoBehaviour
{
    [SerializeField] GameObject leftEye;
    [SerializeField] GameObject rightEye;
    float blinkDelay = .1f;
    float blinkInterval = .5f;
    float blinkMin = 1.5f;
    float blinkMax = 7f;
    // Start is called before the first frame update
    void Start()
    {
        blinkInterval = Random.Range(blinkMin, blinkMax);
        StartCoroutine(BlinkCycle());
    }

    void SetEyeScale(Vector3 targetScale)
    {
        leftEye.transform.localScale = targetScale;
        rightEye.transform.localScale = targetScale;
    }

    IEnumerator TriggerBlink()
    {
        float delayIncrement = blinkDelay / 3;
        Vector3 originalScale = leftEye.transform.localScale;
        Vector3 blinkScaleFirst = new Vector3(1f, .8f, 1f);
        Vector3 blinkScaleMid = new Vector3(1f, .5f, 1f);
        Vector3 blinkScaleLast = new Vector3(1f, .1f, 1f);
        SetEyeScale(blinkScaleFirst);
        yield return new WaitForSeconds(delayIncrement);
        SetEyeScale(blinkScaleMid);
        yield return new WaitForSeconds(delayIncrement);
        SetEyeScale(blinkScaleLast);
        yield return new WaitForSeconds(delayIncrement);
        SetEyeScale(blinkScaleMid);
        yield return new WaitForSeconds(delayIncrement);
        SetEyeScale(blinkScaleFirst);
        yield return new WaitForSeconds(delayIncrement);
        SetEyeScale(originalScale);
    }

    IEnumerator BlinkCycle()
    {
        StartCoroutine(TriggerBlink());
        yield return new WaitForSeconds(blinkInterval);
        blinkInterval = Random.Range(blinkMin, blinkMax);
        StartCoroutine(BlinkCycle());
    }
}
