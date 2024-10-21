using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIntroSequencer : MonoBehaviour
{
    [SerializeField] protected GameObject backgroundShade;
    [SerializeField]
    protected GameObject outerRing;
    [SerializeField]
    protected GameObject midRing;
    [SerializeField]
    protected GameObject runeWrapper;
    [SerializeField]
    protected GameObject runeWrapperBorder;
    [SerializeField]
    protected GameObject pointerArm;
    protected GameObject pointerDot;
    [SerializeField]
    protected GameObject pointerTarget;
    protected RuneAnimationSoundFX runeAnimationSoundFX;

    [HideInInspector]
    public bool winTrigger = false;
    [HideInInspector]
    public bool exitAnimationStarted = false;
    [HideInInspector]
    public bool inputActive = false;

    protected float defaultGlow = 7f;
    protected float defaultGlowSpeed = 3f;

    protected InputStateTracker inputStateTracker;

    [SerializeField] protected GameEvent battleChallengeSuccess;
    [SerializeField] protected GameEvent battleChallengeFailure;

    protected GameObject[] orbitRings;
    protected GameObject[] orbitDots;
    public GameObject[] powerRunes;
    protected float[] orbitScales;

    protected float timeLimit = 10f;

    protected bool debugReset = false;

    protected void FindPOwerRunes()
    {
        List<GameObject> tempPowerRuneList = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("BattleTrigger"))
            {
                tempPowerRuneList?.Add(transform.GetChild(i).gameObject);
            }
        }

        powerRunes = tempPowerRuneList.ToArray();
    }
    protected void FindBattleIndicators()
    {
        List<GameObject> tempOrbitRingsList = new List<GameObject>();

        orbitDots = new GameObject[3];
        orbitScales = new float[3];

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("BattleIndicator"))
            {
                tempOrbitRingsList?.Add(transform.GetChild(i).gameObject);
            }

        }
        orbitRings = tempOrbitRingsList.ToArray();

        for (int i = 0; i <= orbitRings.Length - 1; i++)
        {
            orbitDots[i] = orbitRings[i]?.transform?.GetChild(0)?.gameObject;
            orbitScales[i] = orbitRings[i].transform.localScale.y;
        }

    }

    virtual protected void BattleChallengeReset()
    {
        BattleChallengeCleanup();
    }

    protected void BattleChallengeCleanup()
    {
        exitAnimationStarted = false;
        pointerDot.GetComponent<RotationTweener>().StopAllCoroutines();
        pointerDot.transform.rotation = Quaternion.Euler(0, 0, -45f);

        int targetIndex = 0;
        foreach (GameObject orbitRing in orbitRings)
        {
            orbitRing.GetComponent<UtilityScaleTweener>()?.SetUniformScale(orbitScales[targetIndex]);
            targetIndex++;
        }
    }

    protected void PointerIntroSequence()
    {
        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);

        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(.5f);
        pointerDot.GetComponent<RotationTweener>().TriggerRotation(0f, 1f);
    }

    protected IEnumerator ChallengePartSequence()
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
        StartCoroutine(GlowFadeIn(pointerArm));

        yield return new WaitForSeconds(2.5f);

        // NOTE: this is what kicks off the minor rune countdown sequence
        runeWrapper.GetComponent<AlphaTweenSequencer>().ReverseTweenSequence(timeLimit);
    }

    protected IEnumerator IntroFinalSequence()
    {
        // enable battle challenge input;
        inputActive = true;
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(defaultGlow * 2, defaultGlowSpeed);
        StartCoroutine(CountDown());

        for (float timer = timeLimit; timer >= 0; timer -= Time.deltaTime)
        {
            // NOTE: this is he shortcut to the end of the animation when player has won
            if (winTrigger)
            {
                winTrigger = false;
                StartCoroutine(ExitSequence());
                yield break;
            }
            yield return null;
        }
        if (!exitAnimationStarted)
        {
            StartCoroutine(ExitSequence());
        }
    }

    protected void TriggerIntroSequence()
    {
        StartCoroutine(IntroSequence());
    }
    protected virtual IEnumerator IntroSequence()
    {
        print("Intro implementation missing");
        yield return null;
    }

    protected void PointerExitSequence()
    {
        exitAnimationStarted = true;
        // disabable battle challenge input;
        inputActive = false;
        pointerDot.GetComponent<RotationTweener>().TriggerContinuousRotation(400f);
        pointerArm.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        pointerDot.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
    }

    protected IEnumerator ExitPartsSequence()
    {
        yield return new WaitForSeconds(.5f);

        midRing.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        yield return new WaitForSeconds(.25f);
        midRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        outerRing.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.5f);

        runeWrapperBorder.GetComponent<GlowTweener>().TriggerGlowTween(0, defaultGlowSpeed);
        yield return new WaitForSeconds(.25f);
        runeWrapperBorder.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(0f, 0.25f);
        yield return new WaitForSeconds(.25f);

        pointerTarget?.GetComponent<ColorTweener>()?.TriggerAlphaImageTween(0f);
    }

    protected IEnumerator ExitFinalSequence(bool success = false, bool failure = false)
    {
        yield return new WaitForSeconds(.125f);

        pointerArm.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        pointerDot.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);
        yield return new WaitForSeconds(.25f);

        backgroundShade.GetComponent<ColorTweener>().TriggerAlphaImageTween(0f);

        if (failure)
        {
            StartCoroutine(FailureSequence());
        }
        else if (success)
        {
            StartCoroutine(SuccessSequence());
        }
    }

    protected virtual IEnumerator ExitSequence()
    {
        print("Exit implementation missing");
        yield return null;
    }
    

    //unique to radar wheel but can be defined here
    protected IEnumerator AlphaTweenRunes(float targetAlpha = 0.5f, float runeSpeed = 6f, float runeDelay = .2f, float startDelay = 0f)
    {
        yield return new WaitForSeconds(startDelay);
        int targetIndex = 0;

        foreach (GameObject powerRune in powerRunes)
        {
            powerRune.GetComponent<ColorTweener>().TriggerAlphaImageTween(targetAlpha, runeSpeed);
            yield return new WaitForSeconds(runeDelay);
            targetIndex++;
        }
    }

    protected IEnumerator GlowFadeIn(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetTweener.TriggerAlphaImageTween(1f, 10f);
        yield return new WaitForSeconds(.1f);
        targetGlow.TriggerGlowByDuration(7f, .5f);
    }

    protected IEnumerator GlowFadeOut(GameObject targetObject)
    {
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetObject.GetComponent<GlowTweener>().TriggerGlowTween(0f, 4f);
        yield return new WaitForSeconds(.1f);
        targetTweener.TriggerAlphaImageTween(0.5f, 6f);
    }


    protected IEnumerator CountDown()
    {
        yield return new WaitForSeconds(timeLimit - 2.5f);
        if (!exitAnimationStarted)
        {
            runeAnimationSoundFX.PlayCountDown();
        }
    }

    protected IEnumerator FadeOutOrbitRings()
    {
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
    }
    
    // the time limit can be a shared field that is set in each child class Start method
    // the failure setting code will have to move out of the SuccessSequence
    protected IEnumerator SuccessSequence()
    {
        runeAnimationSoundFX.PlaySpellSuccess();
        battleChallengeSuccess.Invoke();
        StartCoroutine(FadeOutOrbitRings());
        yield return new WaitForSeconds(1f);
        BattleChallengeReset();
    }

    protected IEnumerator FailureSequence()
    {
        runeAnimationSoundFX.PlaySpellFailure();
        yield return new WaitForSeconds(.25f);

        StartCoroutine(FadeOutOrbitRings());

        battleChallengeFailure.Invoke();
        yield return new WaitForSeconds(1f);
        BattleChallengeReset();
    }

}