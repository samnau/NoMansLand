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

    InputStateTracker inputStateTracker;
    void Start()
    {
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;
        pointerDot = pointerArm.transform.parent.gameObject;
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        StartCoroutine(RuneRingIntroSequence());
    }
    IEnumerator RuneRingIntroSequence()
    {
        yield return new WaitForSeconds(.5f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        pointerDot.GetComponent<RotationTweener>().TriggerRotation();
        // TODO: move this to higher palce in the UI code later
        yield return new WaitForSeconds(.5f);
        inputStateTracker.isUiActive = true;
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);

        yield return new WaitForSeconds(.5f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.5f);

        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
        yield return new WaitForSeconds(.5f);

        runeWrapper.GetComponent<AlphaTweenSequencer>().TweenSequence();
        yield return new WaitForSeconds(3f);

        runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence();
        StartCoroutine(RuneRingExitSequence());
        yield return null;
    }
    IEnumerator RuneRingExitSequence()
    {
        RadarSweeperTargetController radarSweeperTargetController = FindObjectOfType<RadarSweeperTargetController>();
        radarSweeperTargetController.StopRotation();

        yield return new WaitForSeconds(radarSweeperTargetController.timeLimit);
        yield return new WaitForSeconds(.5f);

        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        pointerDot.GetComponent<RotationTweener>().TriggerRotation(45f);
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
    }

}
