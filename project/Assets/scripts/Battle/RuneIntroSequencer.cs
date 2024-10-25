using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneIntroSequencer : BattleIntroSequencer
{
    RadarSweeperTargetController radarSweeperTargetController;

    void Start()
    {
        FindBattleIndicators();
        FindPOwerRunes();

        pointerDot = pointerArm.transform.parent.gameObject;
        runeAnimationSoundFX = transform.GetComponent<RuneAnimationSoundFX>();
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        radarSweeperTargetController = GetComponentInChildren<RadarSweeperTargetController>();
    }

    protected override void BattleChallengeReset()
    {
        base.BattleChallengeReset();
        radarSweeperTargetController.failure = false;
        radarSweeperTargetController.success = false;
        radarSweeperTargetController.hitCount = 0;
    }

    protected override IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(.5f);
        PointerIntroSequence();
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow, defaultGlowSpeed);

        StartCoroutine(AlphaTweenRunes(.5f, 6f, .2f, 1.5f));
        StartCoroutine(ChallengePartSequence());

        yield return new WaitForSeconds(3.25f);

        float targetRotation = pointerTarget.GetComponent<RadarSweeperTargetController>().SetPointerTriggerStartRotation();
        pointerTarget.GetComponent<RotationTweener>().SimpleSetRotation(targetRotation);
        //NOTE: This starts the random rune highlight
        pointerTarget.GetComponent<RadarSweeperTargetController>().StartRotation();

        pointerDot.GetComponent<RadarSweeperController>().canSweep = true;

        // NOTE: this is the method that starts the radar sweeper target controller countdown
        radarSweeperTargetController.StartCoroutine(radarSweeperTargetController.TriggerTimeLimit());
        StartCoroutine(CountDown());
        StartCoroutine(IntroFinalSequence());
    }

    protected override IEnumerator ExitSequence()
    {
        PointerExitSequence();
        yield return new WaitForSeconds(.5f);
        radarSweeperTargetController.StopRotation();
        StartCoroutine(ExitPartsSequence());
        yield return new WaitForSeconds(2f);
        StartCoroutine(AlphaTweenRunes(0f, 3.5f, .1f));

        StartCoroutine(ExitFinalSequence(radarSweeperTargetController.success, radarSweeperTargetController.failure));
    }

}
