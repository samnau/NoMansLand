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

    protected override IEnumerator ChallengePartSequence()
    {
        yield return new WaitForSeconds(.25f);
        inputStateTracker.isUiActive = true;
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        runeAnimationSoundFX.PlayRingAppears();

        yield return new WaitForSeconds(.125f);
        runeWrapperBorder.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow, defaultGlowSpeed);
        yield return new WaitForSeconds(.125f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.25f);

        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.25f);

        runeWrapper.GetComponent<AlphaTweenSequencer>().TweenSequence();
        //StartCoroutine(GlowFadeIn(pointerArm));

        yield return new WaitForSeconds(2.5f);

        // NOTE: this is what kicks off the minor rune countdown sequence
        //runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(timeLimit);
    }
    protected override IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(.5f);
        PointerIntroSequence();
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow, defaultGlowSpeed);

        StartCoroutine(AlphaTweenRunes(.5f, 6f, .2f, 1.5f));
        StartCoroutine(ChallengePartSequence());

        CheckForTutorial();

        //yield return new WaitForSeconds(3.25f);

        //float targetRotation = pointerTarget.GetComponent<RadarSweeperTargetController>().SetPointerTriggerStartRotation();
        //pointerTarget.GetComponent<RotationTweener>().SimpleSetRotation(targetRotation);
        ////NOTE: This starts the random rune highlight
        //pointerTarget.GetComponent<RadarSweeperTargetController>().StartRotation();

        //pointerDot.GetComponent<RadarSweeperController>().canSweep = true;

        //// NOTE: this is the method that starts the radar sweeper target controller countdown
        //radarSweeperTargetController.StartCoroutine(radarSweeperTargetController.TriggerTimeLimit());
        //StartCoroutine(CountDown());
        //StartCoroutine(IntroFinalSequence());
    }

    protected override IEnumerator EnableTutorialSequence()
    {
        if (tutorialAnimationSequencer.tutorialCompleted)
        {
            yield return new WaitForSeconds(.5f);

            float targetRotation = pointerTarget.GetComponent<RadarSweeperTargetController>().SetPointerTriggerStartRotation();
            pointerTarget.GetComponent<RotationTweener>().SimpleSetRotation(targetRotation);
            //NOTE: This starts the random rune highlight
            pointerTarget.GetComponent<RadarSweeperTargetController>().StartRotation();

            pointerDot.GetComponent<RadarSweeperController>().canSweep = true;
            runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(timeLimit);

            // NOTE: this is the method that starts the radar sweeper target controller countdown
            radarSweeperTargetController.StartCoroutine(radarSweeperTargetController.TriggerTimeLimit());
            StartCoroutine(CountDown());
            StartCoroutine(IntroFinalSequence());
        }
        else
        {
            StartTutorial();
        }
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
