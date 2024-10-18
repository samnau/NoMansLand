using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerIntroSequencer : BattleIntroSequencer
{
    // unique
    BattleSpinner battleSpinner;

    //unique
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
    }

    void ResetRuneRing()
    {
        exitAnimationStarted = false;
        //NOTE: come back to this later when finalizing the exit animation
        pointerDot.GetComponent<RotationTweener>().StopAllCoroutines();
        pointerDot.transform.rotation = Quaternion.Euler(0, 0, -45f);
        battleSpinner.failure = false;

        // target controller hit count needs to be reset, somewhere
    }

    protected override IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(.5f);
        PointerIntroSequence();
        StartCoroutine(ChallengePartSequence());
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow * .25f, defaultGlowSpeed);
        yield return new WaitForSeconds(1f);
        triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        yield return new WaitForSeconds(3.25f);
        StartCoroutine(CountDown());
        battleSpinner.StartCoroutine(battleSpinner.Timeout());
        // enable battle challenge input;
        inputActive = true;
        pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;
        StartCoroutine(IntroFinalSequence());
    }


    IEnumerator RuneCountDown()
    {
        yield return new WaitForSeconds(timeLimit - 2.5f);
        if (!exitAnimationStarted)
        {
            runeAnimationSoundFX.PlayCountDown();
        }
    }
    protected override IEnumerator ExitSequence()
    {
        pointerTarget.GetComponent<RotationTweener>().TriggerRotation(45f, 1.5f);
        battleSpinner.ToggleRotation();
        PointerExitSequence();
        pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;
        StartCoroutine(ExitPartsSequence());
        yield return new WaitForSeconds(2f);
        triggerRune?.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(0f, .7f);
        StartCoroutine(ExitFinalSequence(battleSpinner.success, battleSpinner.failure));
    }

}
