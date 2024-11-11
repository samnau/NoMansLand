using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerTutorialSequencer : TutorialAnimationSequencer
{
    protected override void StartTutorial()
    {
        spaceBar.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetSpaceBarScale, .25f, .5f);

        exclaimationPoint.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetExclaimationPointScale, .2f);
    }

}
