using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTutorialSequencer : TutorialAnimationSequencer
{

    protected override void StartTutorial()
    {
        spaceBar.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetSpaceBarScale, .125f, 2f);

        exclaimationPoint.GetComponent<UtilityScaleTweener>().TriggerScaleLooper(targetExclaimationPointScale, .25f);

    }
}
