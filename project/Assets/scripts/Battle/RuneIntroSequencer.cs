using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneIntroSequencer : MonoBehaviour
{
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
    [SerializeField]
    GameObject powerRune1;
    [SerializeField]
    GameObject powerRune2;
    [SerializeField]
    GameObject powerRune3;
    [SerializeField]
    GameObject powerRune4;
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

    InputStateTracker inputStateTracker;
    void Start()
    {
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;
        pointerDot = pointerArm.transform.parent.gameObject;
        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        StartCoroutine(RuneRingIntroSequence());
        radarSweeperTargetController = FindObjectOfType<RadarSweeperTargetController>();
    }

    float SetPointerStartRotation()
    {
        var randomModfier = Random.Range(0, 10);
        float rotationMultiplier = Random.Range(1, 4) * 1f;
        float rotationModfier = randomModfier < 5 ? -1f : 1f;
        return 90f * rotationMultiplier * rotationModfier;
    }
    IEnumerator RuneRingIntroSequence()
    {
        yield return new WaitForSeconds(.5f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerDot.GetComponent<RotationTweener>().TriggerRotation(0f);

        // TODO: move this to higher palce in the UI code later
        yield return new WaitForSeconds(.25f);
        inputStateTracker.isUiActive = true;
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        runeAnimationSoundFX.PlayRingAppears();

        yield return new WaitForSeconds(.25f);
        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.25f);

        StartCoroutine(RevealRunes());
        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.25f);

        runeWrapper.GetComponent<AlphaTweenSequencer>().TweenSequence();
        yield return new WaitForSeconds(2.5f);
        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);

        yield return new WaitForSeconds(.5f);


        runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence();

        pointerTarget.GetComponent<RotationTweener>().TriggerRotation(SetPointerStartRotation());
        pointerTarget.GetComponent<RadarSweeperTargetController>().StartRotation();

        pointerDot.GetComponent<RadarSweeperController>().canSweep = true;


        radarSweeperTargetController.StartCoroutine(radarSweeperTargetController.TriggerFailure());

        StartCoroutine(RuneCountDown());

        //yield return new WaitForSeconds(radarSweeperTargetController.timeLimit);
        for (float timer = radarSweeperTargetController.timeLimit; timer >= 0; timer -= Time.deltaTime)
        {
            if (winTrigger)
            {
                winTrigger = false;
                StartCoroutine(RuneRingExitSequence());
                yield break;
            }
            yield return null;
        }
        if(!exitAnimationStarted)
        {
            StartCoroutine(RuneRingExitSequence());
        }
    }

    IEnumerator RevealRunes()
    {
        float runeSpeed = 6f;
        float runeDelay = .2f;
        powerRune1.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f, runeSpeed);
        yield return new WaitForSeconds(runeDelay);

        powerRune4.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f, runeSpeed);
        yield return new WaitForSeconds(runeDelay);

        powerRune3.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f, runeSpeed);
        yield return new WaitForSeconds(runeDelay);

        powerRune2.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f, runeSpeed);
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

        pointerDot.GetComponent<RotationTweener>().TriggerContinuousRotation(400f);
        yield return new WaitForSeconds(.5f);
        radarSweeperTargetController.StopRotation();

        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        yield return new WaitForSeconds(.125f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        float runeSpeed = 3.5f;
        float runeDelay = .1f;
        powerRune1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f,runeSpeed);
        yield return new WaitForSeconds(runeDelay);

        powerRune4.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, runeSpeed);
        yield return new WaitForSeconds(runeDelay);

        powerRune3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, runeSpeed);
        yield return new WaitForSeconds(runeDelay);

        powerRune2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f, runeSpeed);
        if(radarSweeperTargetController.failure)
        {
            StartCoroutine(RuneFailureSequence());
        } else if (radarSweeperTargetController.success)
        {
            StartCoroutine(RuneSuccessSequence());
        }
    }

    IEnumerator RuneSuccessSequence()
    {
        runeAnimationSoundFX.PlaySpellSuccess();
        yield return null;
    }

    IEnumerator RuneFailureSequence()
    {
        print("fail sequence?");
        runeAnimationSoundFX.PlaySpellFailure();
        yield return new WaitForSeconds(.25f);
        orbitDot1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        orbitRing1.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        yield return new WaitForSeconds(.25f);

        orbitDot2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        orbitRing2.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        yield return new WaitForSeconds(.25f);

        orbitDot3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
        orbitRing3.GetComponent<ColorTweener>().TriggerAlphaImageTween(0);
    }

}
