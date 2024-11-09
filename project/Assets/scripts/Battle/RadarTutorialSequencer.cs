using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTutorialSequencer : TutorialAnimationSequencer
{
    [SerializeField]
    GameObject leftArrow;
    [SerializeField]
    GameObject rightArrow;
    [SerializeField]
    GameObject plusSign;
    float targetLeftArrowScale;
    float targetRightArrowScale;

    protected override void StartTutorial()
    {
        targetLeftArrowScale = GetTargetTweenScale(leftArrow);

        targetRightArrowScale = GetTargetTweenScale(rightArrow);

        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        leftArrow.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetLeftArrowScale, .25f);
        yield return new WaitForSeconds(.5f);
        rightArrow.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetRightArrowScale, .25f);
        yield return new WaitForSeconds(.5f);
        spaceBar.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetSpaceBarScale, .25f);
        yield return new WaitForSeconds(.5f);
        exclaimationPoint.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetExclaimationPointScale, .25f);

    }

}
