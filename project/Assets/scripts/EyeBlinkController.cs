using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinkController : MonoBehaviour
{
    public float blinkMin = 0.14f;
    public float blinkMax = 4.0f;
    public bool blinkActive = true;
    float currentInterval = 0f;
    Animator targetAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentInterval = setBlinkInterval();
        targetAnimator = GetComponent<Animator>();
        targetAnimator.SetBool("blink", false);
        //startBlinkCycle();
        Debug.Log("blink time");
        StartCoroutine("triggerBlink");
    }

    public void startBlinkCycle()
    {
        Debug.Log("blink start");
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
        Debug.Log("blink trigger");
        targetAnimator.SetBool("blink", true);
        yield return new WaitForSeconds(.2f);
        targetAnimator.SetBool("blink", false);
        currentInterval = setBlinkInterval();
        yield return new WaitForSeconds(currentInterval);
        startBlinkCycle();
    }

    // Update is called once per frame
    void Update()
    {
        //startBlinkCycle();
    }
}
