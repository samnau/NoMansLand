using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinkController : MonoBehaviour
{
    public float blinkMin = 2.0f;
    public float blinkMax = 6.0f;
    public bool blinkActive = true;
    float currentInterval = 0f;
    Animator targetAnimator;

    void Start()
    {
        currentInterval = setBlinkInterval();
        targetAnimator = GetComponent<Animator>();
        targetAnimator.SetBool("blink", false);
        StartCoroutine("triggerBlink");
    }

    public void startBlinkCycle()
    {
        if(blinkActive)
        {
            StartCoroutine("triggerBlink");
        } else
        {
            StopCoroutine("triggerBlink");
        }
    }

    float setBlinkInterval()
    {
        return Random.Range(blinkMin, blinkMax);
    }
  
    IEnumerator triggerBlink()
    {
        targetAnimator.SetBool("left", false);
        targetAnimator.SetBool("blink", true);
        yield return new WaitForSeconds(.2f);
        targetAnimator.SetBool("blink", false);
        currentInterval = setBlinkInterval();
        yield return new WaitForSeconds(currentInterval);
        startBlinkCycle();
    }

}
