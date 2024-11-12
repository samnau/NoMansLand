using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerIntroSequencer : BattleIntroSequencer
{
    BattleSpinner battleSpinner;
    GameObject triggerRune;

    void Start()
    {
        FindBattleIndicators();

        pointerDot = pointerArm.transform.parent.gameObject;
        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();
        //NOTE: this is used to disable the player
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        battleSpinner = pointerTarget.transform.parent.GetComponentInChildren<BattleSpinner>();
        timeLimit = battleSpinner.timeLimit;

        foreach (GlowTweener targetChild in pointerTarget.GetComponentsInChildren<GlowTweener>())
        {
            if(targetChild.transform.gameObject.tag == "BattleTrigger")
            {
                triggerRune = targetChild.transform.gameObject;
            }
        }
    }

    protected override void BattleChallengeReset()
    {
        base.BattleChallengeReset();
        battleSpinner.failure = false;
        battleSpinner.success = false;
        battleSpinner.hitCount = 0;
        battleSpinner.DisableRotation();
        battleSpinner.inputActive = false;
        battleSpinner.ResetRotationSpeed();
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
        StartCoroutine(ChallengePartSequence());
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow * .5f, defaultGlowSpeed);
        yield return new WaitForSeconds(1f);
        triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        CheckForTutorial();
        //yield return new WaitForSeconds(3.25f);
        //StartCoroutine(CountDown());
        //battleSpinner.StartCoroutine(battleSpinner.Timeout());
        //// enable battle challenge input;
        //inputActive = true;
        //pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;
        //battleSpinner.GetComponent<BattleSpinner>().inputActive = inputActive;
        //StartCoroutine(IntroFinalSequence());
    }

    protected override IEnumerator EnableTutorialSequence()
    {
        float targetDelay = tutorialInactive ? 3.25f : .5f;

        if (tutorialAnimationSequencer.tutorialCompleted)
        {
            yield return new WaitForSeconds(targetDelay);
            StartCoroutine(CountDown());
            battleSpinner.StartCoroutine(battleSpinner.Timeout());
            // enable battle challenge input;
            inputActive = true;
            runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(timeLimit);
            pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;
            battleSpinner.GetComponent<BattleSpinner>().inputActive = inputActive;
            StartCoroutine(IntroFinalSequence());
            tutorialInactive = true;
        }
        else
        {
            StartTutorial();
        }
    }

    protected override IEnumerator ExitSequence()
    {
        PointerExitSequence();
        pointerTarget.GetComponent<RotationTweener>().TriggerRotation(45f, 1.5f);
        battleSpinner.ToggleRotation();
        pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;
        StartCoroutine(ExitPartsSequence());
        yield return new WaitForSeconds(2f);
        triggerRune?.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(0f, .7f);
        StartCoroutine(ExitFinalSequence(battleSpinner.success, battleSpinner.failure));
    }

}
