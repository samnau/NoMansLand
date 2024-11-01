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

    protected override IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(.5f);
        PointerIntroSequence();
        StartCoroutine(ChallengePartSequence());
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow * .5f, defaultGlowSpeed);
        yield return new WaitForSeconds(1f);
        triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        yield return new WaitForSeconds(3.25f);
        StartCoroutine(CountDown());
        battleSpinner.StartCoroutine(battleSpinner.Timeout());
        // enable battle challenge input;
        inputActive = true;
        pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;
        battleSpinner.GetComponent<BattleSpinner>().inputActive = inputActive;
        StartCoroutine(IntroFinalSequence());
        print($"input is active: {battleSpinner.GetComponent<BattleSpinner>().inputActive}");
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
