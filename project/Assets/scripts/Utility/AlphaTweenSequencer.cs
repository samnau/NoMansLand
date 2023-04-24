using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTweenSequencer : MonoBehaviour
{
    ColorTweener[] targetTweeners;
    RadarSweeperTargetController radarSweeperTargetController;
    void Start()
    {
        targetTweeners = gameObject.GetComponentsInChildren<ColorTweener>();
        radarSweeperTargetController = FindObjectOfType<RadarSweeperTargetController>();
    }
    public void TweenSequence()
    {
        for (int index = 0; index < targetTweeners.Length; index++)
         {
           StartCoroutine(DelayedAlphaTween(.05f, index, targetTweeners[index], 1f));
         }

    }

    public void ReverseTweenSequence()
    {
        float timeLimit = radarSweeperTargetController.timeLimit;
        float delay = timeLimit / targetTweeners.Length;
        for(int index = targetTweeners.Length-1; index > -1; index--)
        {
            StartCoroutine(DelayedAlphaTween(delay, index, targetTweeners[index], 0f));
        }
    }

    IEnumerator DelayedAlphaTween(float delay, int delayFactor, ColorTweener tweenTarget, float targetAlpha)
    {
        yield return new WaitForSeconds(delay*delayFactor);
        tweenTarget.TriggerAlphaImageTween(targetAlpha, 1.5f);
    }

}
