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

    InputStateTracker inputStateTracker;
    void Start()
    {
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        StartCoroutine(RuneRingIntroSequence());
    }
    IEnumerator RuneRingIntroSequence()
    {
        yield return new WaitForSeconds(.5f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(1f);
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
        yield return new WaitForSeconds(1f);
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
