using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerIntroSequencer : MonoBehaviour
{
    [SerializeField] GameObject backgroundShade;
    [SerializeField]
    GameObject outerRing;
    [SerializeField]
    GameObject midRing;
    [SerializeField]
    GameObject runeWrapper;
    [SerializeField]
    GameObject runeWrapperBorder;
    [SerializeField]
    GameObject orbitRing1;
    [SerializeField]
    GameObject orbitRing2;
    [SerializeField]
    GameObject orbitRing3;
    GameObject orbitDot1;
    GameObject orbitDot2;
    GameObject orbitDot3;
    //[SerializeField]
    //GameObject powerRune1;
    //[SerializeField]
    //GameObject powerRune2;
    //[SerializeField]
    //GameObject powerRune3;
    //[SerializeField]
    //GameObject powerRune4;
    [SerializeField]
    GameObject pointerArm;
    GameObject pointerDot;
    [SerializeField]
    GameObject pointerTarget;
    RuneAnimationSoundFX runeAnimationSoundFX;
    RadarSweeperTargetController radarSweeperTargetController;
    BattleSpinner battleSpinner;
    [HideInInspector]
    public bool winTrigger = false;
    [HideInInspector]
    public bool exitAnimationStarted = false;
    [HideInInspector]
    public bool inputActive = false;

    float defaultGlow = 7f;
    float defaultGlowSpeed = 3f;
    float defaultTargetRuneRotation;

    InputStateTracker inputStateTracker;

    [SerializeField] GameEvent battleChallengeSuccess;
    [SerializeField] GameEvent battleChallengeFailure;

    GameObject triggerRune;

    bool debugReset = false;

    void Start()
    {
        //NOTE: imitate the tag based logic in BattleSpinner to identify these dots and rings
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;
        pointerDot = pointerArm.transform.parent.gameObject;
        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();
        //NOTE: this is used to disable the player
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        battleSpinner = pointerTarget.transform.parent.GetComponentInChildren<BattleSpinner>();

        defaultTargetRuneRotation = pointerDot.transform.rotation.z;

        foreach (GlowTweener targetChild in pointerTarget.GetComponentsInChildren<GlowTweener>())
        {
            if(targetChild.transform.gameObject.tag == "BattleTrigger")
            {
                triggerRune = targetChild.transform.gameObject;
            }
        }
        //radarSweeperTargetController = FindObjectOfType<RadarSweeperTargetController>();
    }

    public void TriggerIntroSequence()
    {
        StartCoroutine(RuneRingIntroSequence());
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
    IEnumerator RuneRingIntroSequence()
    {
        print("rune ring intro!");
        yield return new WaitForSeconds(.5f);

        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        // TODO: tie this boolean into an event instead
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

        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerDot.GetComponent<RotationTweener>().TriggerRotation(0f, 1f);


        pointerTarget?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        triggerRune?.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        runeWrapper.GetComponent<AlphaTweenSequencer>().TweenSequence();
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(GlowFadeIn(pointerArm));
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow*2, defaultGlowSpeed);

        // NOTE: this is what kicks off the minor rune countdown sequence
        runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(battleSpinner.timeLimit);

        // NOTE: this is the method that starts the radar sweeper target controller countdown
        // COMEBACK: will need to evaluate the battle spinner equivalent
        //radarSweeperTargetController.StartCoroutine(radarSweeperTargetController.TriggerFailure());


        // enable battle challenge input;
        inputActive = true;
        pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;

        StartCoroutine(RuneCountDown());
        battleSpinner.StartCoroutine(battleSpinner.Timeout());

        for (float timer = battleSpinner.timeLimit; timer >= 0; timer -= Time.deltaTime)
        {
            if (winTrigger)
            {
                winTrigger = false;
                StartCoroutine(RuneRingExitSequence());
                yield break;
            }
            yield return null;
        }
        if (!exitAnimationStarted)
        {
            StartCoroutine(RuneRingExitSequence());
        }



    }
    
    //NOTE: candidate for the refactor
    IEnumerator GlowFadeIn(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetTweener.TriggerAlphaImageTween(1f, 10f);
        yield return new WaitForSeconds(.1f);
        targetGlow.TriggerGlowTween(7f, 4f);
    }

    //NOTE: candidate for the refactor
    IEnumerator GlowFadeOut(GameObject targetObject)
    {
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetObject.GetComponent<GlowTweener>().TriggerGlowTween(0f, 4f);
        yield return new WaitForSeconds(.1f);
        targetTweener.TriggerAlphaImageTween(0.5f, 6f);
    }


    IEnumerator RuneCountDown()
    {
        yield return new WaitForSeconds(battleSpinner.timeLimit - 2.5f);
        if (!exitAnimationStarted)
        {
            runeAnimationSoundFX.PlayCountDown();
        }
    }
    IEnumerator RuneRingExitSequence()
    {
        exitAnimationStarted = true;
        // disabable battle challenge input;
        inputActive = false;
        pointerDot.GetComponent<BattleRingTrigger>().inputActive = inputActive;


        pointerTarget.GetComponent<RotationTweener>().TriggerRotation(45f, 1.5f);
        // start the pointer spinning as it fades out
        battleSpinner.ToggleRotation();
        pointerDot.GetComponent<RotationTweener>().TriggerContinuousRotation(400f);
        pointerArm.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);

        yield return new WaitForSeconds(.5f);

        midRing.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        yield return new WaitForSeconds(.25f);
        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        yield return new WaitForSeconds(.25f);
        runeWrapperBorder.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(0f,0.25f);
        yield return new WaitForSeconds(.25f);

        pointerTarget?.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        triggerRune?.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(0f, .7f);

        yield return new WaitForSeconds(.125f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);


        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);

        if (battleSpinner.failure)
        {
            StartCoroutine(RuneFailureSequence());
        }
        else if (battleSpinner.success)
        {
            StartCoroutine(RuneSuccessSequence());
        }

    }

    IEnumerator RuneSuccessSequence()
    {
        runeAnimationSoundFX.PlaySpellSuccess();
        battleChallengeSuccess.Invoke();
        ResetRuneRing();
        battleSpinner.TriggerOrbitRingReset();
        yield return null;
        //yield return new WaitForSeconds(7f);
        //RuneRingIntroSequence();
    }

    IEnumerator RuneFailureSequence()
    {
        runeAnimationSoundFX.PlaySpellFailure();
        yield return new WaitForSeconds(.25f);

        orbitDot1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
        orbitRing1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
        yield return new WaitForSeconds(.125f);

        orbitDot1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        orbitRing1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        yield return new WaitForSeconds(.125f);

        orbitDot2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
        orbitRing2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
        yield return new WaitForSeconds(.125f);
        orbitDot2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        orbitRing2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        yield return new WaitForSeconds(.125f);

        orbitDot3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
        orbitRing3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
        yield return new WaitForSeconds(.125f);

        orbitDot3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        orbitRing3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);

        battleChallengeFailure.Invoke();

        ResetRuneRing();
    }

}
