using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneIntroSequencer : MonoBehaviour
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
    GameObject pointerArm;
    GameObject pointerDot;
    [SerializeField]
    GameObject pointerTarget;
    RuneAnimationSoundFX runeAnimationSoundFX;
    RadarSweeperTargetController radarSweeperTargetController;
    [HideInInspector]
    public bool winTrigger = false;
    [HideInInspector]
    public bool exitAnimationStarted = false;
    [HideInInspector]
    public bool inputActive = false;

    float defaultGlow = 7f;
    float defaultGlowSpeed = 3f;

    InputStateTracker inputStateTracker;

    [SerializeField] GameEvent battleChallengeSuccess;
    [SerializeField] GameEvent battleChallengeFailure;

    GameObject[] orbitRings;
    GameObject[] orbitDots;
    GameObject[] powerRunes;
    float[] orbitScales;

    bool debugReset = false;

    void Start()
    {
        List<GameObject> tempOrbitRingsList = new List<GameObject>();
        List<GameObject> tempPowerRuneList = new List<GameObject>();

        orbitDots = new GameObject[3];
        orbitScales = new float[3];

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("BattleIndicator"))
            {
                tempOrbitRingsList?.Add(transform.GetChild(i).gameObject);
            }
            if (transform.GetChild(i).CompareTag("BattleTrigger"))
            {
                tempPowerRuneList?.Add(transform.GetChild(i).gameObject);
            }
        }
        orbitRings = tempOrbitRingsList.ToArray();

        powerRunes = tempPowerRuneList.ToArray();

        for (int i = 0; i <= orbitRings.Length - 1; i++)
        {
            orbitDots[i] = orbitRings[i]?.transform?.GetChild(0)?.gameObject;
            orbitScales[i] = orbitRings[i].transform.localScale.y;
        }

        pointerDot = pointerArm.transform.parent.gameObject;
        runeAnimationSoundFX = transform.GetComponent<RuneAnimationSoundFX>();
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        radarSweeperTargetController = GetComponentInChildren<RadarSweeperTargetController>();
    }

    public void TriggerIntroSequence()
    {
        StartCoroutine(RuneRingIntroSequence());
    }

    // REFACTOR: this method shouldn't be part of the intro sequence code
    float SetPointerTriggerStartRotation()
    {
        var randomModfier = Random.Range(0, 10);
        float rotationMultiplier = Random.Range(1, 4) * 1f;
        float rotationModfier = randomModfier < 5 ? -1f : 1f;
        return 90f * rotationMultiplier * rotationModfier;
    }

    void ResetRuneRing()
    {
        exitAnimationStarted = false;
        pointerDot.GetComponent<RotationTweener>().StopAllCoroutines();
        pointerDot.transform.rotation = Quaternion.Euler(0, 0, -45f);
        radarSweeperTargetController.failure = false;

        int targetIndex = 0;
        foreach(GameObject orbitRing in orbitRings)
        {
            orbitRing.GetComponent<UtilityScaleTweener>()?.SetUniformScale(orbitScales[targetIndex]);
            targetIndex++;
        }

        // target controller hit count needs to be reset, somewhere
        //TriggerIntroSequence();
    }
    IEnumerator RuneRingIntroSequence()
    {
        yield return new WaitForSeconds(.5f);

        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerDot.GetComponent<RotationTweener>().TriggerRotation(0f, 1f);
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow, defaultGlowSpeed);

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

        StartCoroutine(AlphaTweenRunes());
        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.25f);


        runeWrapper.GetComponent<AlphaTweenSequencer>().TweenSequence();
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(GlowFadeIn(pointerArm));

        // NOTE: this is what kicks off the minor rune countdown sequence
        runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(radarSweeperTargetController.timeLimit);
        float targetRotation = pointerTarget.GetComponent<RadarSweeperTargetController>().SetPointerTriggerStartRotation();
        pointerTarget.GetComponent<RotationTweener>().SimpleSetRotation(targetRotation);
        //NOTE: This starts the random rune highlight
        pointerTarget.GetComponent<RadarSweeperTargetController>().StartRotation();

        pointerDot.GetComponent<RadarSweeperController>().canSweep = true;

        // NOTE: this is the method that starts the radar sweeper target controller countdown
        radarSweeperTargetController.StartCoroutine(radarSweeperTargetController.TriggerFailure());


        // enable battle challenge input;
        inputActive = true;

        StartCoroutine(RuneCountDown());

        // NOTE: Access point for turning off time limit for testing
        //yield break;

        for (float timer = radarSweeperTargetController.timeLimit; timer >= 0; timer -= Time.deltaTime)
        {
            // NOTE: this is he shortcut to the end of the animation when player has won
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
    IEnumerator AlphaTweenRunes(float targetAlpha = 0.5f, float runeSpeed = 6f, float runeDelay = .2f )
    {
        int targetIndex = 0;

        foreach (GameObject powerRune in powerRunes)
        {
            powerRune.GetComponent<ColorTweener>().TriggerAlphaImageTween(targetAlpha, runeSpeed);
            yield return new WaitForSeconds(runeDelay);
            targetIndex++;
        }
    }

    IEnumerator GlowFadeIn(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetTweener.TriggerAlphaImageTween(1f, 10f);
        yield return new WaitForSeconds(.1f);
        targetGlow.TriggerGlowTween(7f, 4f);
    }

    IEnumerator GlowFadeOut(GameObject targetObject)
    {
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetObject.GetComponent<GlowTweener>().TriggerGlowTween(0f, 4f);
        yield return new WaitForSeconds(.1f);
        targetTweener.TriggerAlphaImageTween(0.5f, 6f);
    }


    IEnumerator RuneCountDown()
    {
        yield return new WaitForSeconds(radarSweeperTargetController.timeLimit - 2.5f);
        if(!exitAnimationStarted)
        {
            runeAnimationSoundFX.PlayCountDown();
        }
    }
    IEnumerator RuneRingExitSequence()
    {
        exitAnimationStarted = true;
        // disabable battle challenge input;
        inputActive = false;

        // start the pointer spinning as it fades out
        pointerDot.GetComponent<RotationTweener>().TriggerContinuousRotation(400f);
        yield return new WaitForSeconds(.5f);
        radarSweeperTargetController.StopRotation();

        midRing.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        yield return new WaitForSeconds(.25f);
        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        yield return new WaitForSeconds(.25f);
        runeWrapperBorder.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        pointerArm.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);

        yield return new WaitForSeconds(.125f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        StartCoroutine(AlphaTweenRunes(0f, 3.5f, .1f));

        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);


        if (radarSweeperTargetController.failure)
        {
            StartCoroutine(RuneFailureSequence());
        } else if (radarSweeperTargetController.success)
        {
            StartCoroutine(RuneSuccessSequence());
        }

        // TEMP: just for working on the reset code for the ring
        //debugReset = true;
    }

    IEnumerator RuneSuccessSequence()
    {
        runeAnimationSoundFX.PlaySpellSuccess();
        battleChallengeSuccess.Invoke();
        ResetRuneRing();
        yield return null;
    }

    IEnumerator RuneFailureSequence()
    {
        runeAnimationSoundFX.PlaySpellFailure();
        yield return new WaitForSeconds(.25f);

        int targetIndex = 0;

        foreach (GameObject orbitRing in orbitRings)
        {
            orbitRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
            orbitDots[targetIndex].GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, defaultGlowSpeed);
            yield return new WaitForSeconds(.125f);
            orbitRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
            orbitDots[targetIndex].GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
            targetIndex++;
        }

        battleChallengeFailure.Invoke();
        yield return new WaitForSeconds(1f);
        ResetRuneRing();
    }

}
