using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTutorialSequencer : TutorialAnimationSequencer
{

    protected override void StartTutorial()
    {
        spaceBar.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetSpaceBarScale, .125f, .25f);

        exclaimationPoint.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetExclaimationPointScale, .2f);

    }
}
