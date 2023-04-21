using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTweenSequencer : MonoBehaviour
{
    ColorTweener[] targetTweeners;
    // Start is called before the first frame update
    void Start()
    {
        targetTweeners = gameObject.GetComponentsInChildren<ColorTweener>();
       // TweenSequence(targetTweeners);
    }
    public void TweenSequence()
    {
        for (int index = 0; index < targetTweeners.Length; index++)
         {
           StartCoroutine(DelayedAlphaTween(.05f, index, targetTweeners[index]));
         }

    }

    IEnumerator DelayedAlphaTween(float delay, int delayFactor, ColorTweener tweenTarget)
    {
        yield return new WaitForSeconds(delay*delayFactor);
        tweenTarget.TriggerAlphaImageTween(1f, 1.5f);
    }

}
