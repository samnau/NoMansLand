using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIntroSequencer : BattleIntroSequencer
{
    GameObject triggerRune;
    PowerCharger powerCharger;
    //NOTE: should be able to type the powerCharger and others like it to generic battle challenge type for abstraction

    void Start()
    {
        FindBattleIndicators();
        powerCharger = pointerTarget.transform.parent.GetComponentInChildren<PowerCharger>();
        //NOTE: refactor?
        // these are shared
        // also why is the arm the main reference and not the parent dot?
        pointerDot = pointerArm.transform.parent.gameObject;
        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();

        timeLimit = powerCharger.timeLimit;

        inputStateTracker = FindObjectOfType<InputStateTracker>();


        foreach (GlowTweener targetChild in pointerTarget.GetComponentsInChildren<GlowTweener>())
        {
            if (targetChild.transform.gameObject.tag == "BattleTrigger")
            {
                triggerRune = targetChild.transform.gameObject;
            }
        }
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
        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        pointerDot.GetComponent<RotationTweener>().SimpleSetRotation(-180f);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(ChallengePartSequence());
        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        yield return new WaitForSeconds(1f);
        triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);

        //yield return new WaitForSeconds(2.75f);
        //StartCoroutine(GlowFadeIn(pointerArm));
        //pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow, defaultGlowSpeed);

        //yield return new WaitForSeconds(.5f);

        //powerCharger.StartCoroutine(powerCharger.RotateTriggerWrapper());
        //powerCharger.SetPointerColors();

        //StartCoroutine(EnableTutorialSequence());
        CheckForTutorial();

        //StartCoroutine(CountDown());
        //powerCharger.StartCoroutine(powerCharger.Timeout());

        //pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        //pointerDot.GetComponent<RotationTweener>().TriggerRotation(0f, 1f);
        // enable battle challenge input;
        //inputActive = true;
        //powerCharger.inputActive = inputActive;
        //powerCharger.challengeActive = true;
        //triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        //triggerRune?.GetComponent<GlowTweener>().TriggerGlowByDuration(2f, .5f);

        //StartCoroutine(IntroFinalSequence());
    }

    protected override IEnumerator EnableTutorialSequence()
    {
        if(tutorialAnimationSequencer.tutorialCompleted)
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(GlowFadeIn(pointerArm));
            pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow, defaultGlowSpeed);

            yield return new WaitForSeconds(.5f);
            powerCharger.StartCoroutine(powerCharger.RotateTriggerWrapper());
            powerCharger.SetPointerColors();

            runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(timeLimit);
            //StartCoroutine(CountDown());
            powerCharger.StartCoroutine(powerCharger.Timeout());

            pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
            pointerDot.GetComponent<RotationTweener>().TriggerRotation(0f, 1f);

            // enable battle challenge input;
            inputActive = true;
            powerCharger.inputActive = inputActive;
            powerCharger.challengeActive = true;
            triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
            triggerRune?.GetComponent<GlowTweener>().TriggerGlowByDuration(2f, .5f);

            StartCoroutine(IntroFinalSequence());
            yield return null;
        } else
        {
            StartTutorial();
        }
    }

    protected override IEnumerator ExitSequence()
    {
        PointerExitSequence();
        triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        triggerRune?.GetComponent<GlowTweener>().TriggerGlowByDuration(0f, .5f);

        pointerTarget.GetComponent<RotationTweener>().TriggerRotation(45f, 1.5f);
        StartCoroutine(ExitPartsSequence());
        yield return new WaitForSeconds(2f);
        triggerRune?.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(0f, .7f);
        powerCharger.challengeActive = false;
        StartCoroutine(ExitFinalSequence(powerCharger.success, powerCharger.failure));
    }

    protected override void BattleChallengeReset()
    {
        base.BattleChallengeReset();
        powerCharger.failure = false;
        powerCharger.success = false;
        powerCharger.hitCount = 0;
        powerCharger.inputActive = false;
        powerCharger.challengeActive = false;
    }
}
