using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTweenSequencer : MonoBehaviour
{
    ColorTweener[] targetTweeners;
    RadarSweeperTargetController radarSweeperTargetController;
    [SerializeField] Color targetGlowColor = Color.blue;
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

    public void FastForwardReverseTween(int currentIndex, float currentDelay)
    {

    }

    IEnumerator DelayedAlphaTween(float delay, int delayFactor, ColorTweener tweenTarget, float targetAlpha)
    {
        GlowTweener targetGlow = tweenTarget.GetComponent<GlowTweener>();
        yield return new WaitForSeconds(delay*delayFactor);
        tweenTarget.TriggerAlphaImageTween(targetAlpha, 1.5f);
        if(targetAlpha == 1f)
        {
            //targetGlow.SetGlowColor(Color.blue);
            targetGlow.SetGlowColor(targetGlowColor);
            yield return new WaitForSeconds(.1f);
            targetGlow.TriggerGlowTween(20f, 4f);
            yield return new WaitForSeconds(.2f);
            targetGlow.TriggerGlowTween(0, 4f);
        }

    }

}
