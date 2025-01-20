using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class BattleTargetController : BattleChallenge
{
    float rotationMultiplier;
    float rotationModfier = 1f;
    float rotationBase = 90f;
    float targetRotaton;
    public GameObject battleTrigger;
    //[HideInInspector] public int hitCount = 0;
    public bool hitActive = false;
    //public int hitSuccessLimit = 4;

    public float triggerTimeLimit = 2f;
    // Orbit rings will be shared
    [SerializeField]
    GameObject orbitRing1;
    [SerializeField]
    GameObject orbitRing2;
    [SerializeField]
    GameObject orbitRing3;
    GameObject orbitDot1;
    GameObject orbitDot2;
    GameObject orbitDot3;

    float orbitRing1Scale;
    float orbitRing2Scale;
    float orbitRing3Scale;


    //GameObject[] orbitRings;
    //GameObject[] orbitDots;
    GameObject[] powerRunes;
    //float[] orbitScales;

    GameObject targetRune;

    [SerializeField]
    GameObject pointerArm;

    bool hitInerruption = false;
    bool stopRotation = false;
    bool hitAttempted = false;
    [SerializeField] bool isRuneSpinner = false;

    int hitAttamptCount = 0;

    [SerializeField] GameEvent cameraShake;

    //RuneAnimationSoundFX runeAnimationSoundFX;

    RuneIntroSequencer runeIntroSequencer;

    void Start()
    {
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;

        orbitRings = new GameObject[] { orbitRing1, orbitRing2, orbitRing3 };
        orbitDots = new GameObject[] { orbitDot1, orbitDot2, orbitDot3 };

        //TODO: the will only be BattleTrigger items in the radar sweeper
        powerRunes = GameObject.FindGameObjectsWithTag("BattleTrigger");

        //NOTE: anything related to orbit rings will be shared

        orbitRing1Scale = orbitRing1.transform.localScale.y;
        orbitRing2Scale = orbitRing2.transform.localScale.y;
        orbitRing3Scale = orbitRing3.transform.localScale.y;

        orbitScales = new float[] { orbitRing1Scale, orbitRing2Scale, orbitRing3Scale };

        // These will be shared
        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();
        runeIntroSequencer = FindObjectOfType<RuneIntroSequencer>();

        // This will need multiple implementations for each challenge
        SetTargetRotation();
    }

    void SetTargetRotation()
    {
        print("setting target rotation");
        var randomModfier = Random.Range(0, 10);
        rotationMultiplier = Random.Range(1, 4) * 1f;
        rotationModfier = randomModfier < 5 ? -1f : 1f;
        // NOTE: altering the rotation behavior is this is used with a Rune Spinner
        if (isRuneSpinner)
        {
            print("rune spinner active");
        }
        targetRotaton = 90f;
        //targetRotaton = isRuneSpinner ? 0f : rotationBase * rotationMultiplier * rotationModfier;
        print($"target rotation is: {targetRotaton}");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitAttamptCount = 0;
        if (collision.gameObject.name == battleTrigger.name)
        {
            hitActive = true;
        }

        if (collision.CompareTag("BattleTrigger"))
        {
            ResetSuccessHightlight();
            StartCoroutine(HighlightRune(collision.gameObject));
            targetRune = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == battleTrigger.name)
        {
            hitActive = false;
            hitAttempted = false;
        }
        if (collision.CompareTag("BattleTrigger"))
        {
            StartCoroutine(UnHighlightRune(collision.gameObject));
            //            ColorTweener targetTweener = collision.gameObject.GetComponent<ColorTweener>();
            //            targetTweener.TriggerAlphaImageTween(0.5f, 10);
        }
    }

    //TODO: researcch this warning
    IEnumerator UnHighlightRune(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();

        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        //targetGlow.TurnOffGlow();
        //targetGlow.SetGlowColor(Color.white);

        targetObject.GetComponent<GlowTweener>().TriggerGlowTween(0f, 15f);
        yield return new WaitForSeconds(.1f);
        //targetTweener.SetImageAlpha(.5f);
        targetTweener.TriggerAlphaImageTween(0.5f, 10f);
        //ResetHightLight();
    }

    void ResetHightLight()
    {
        foreach (GameObject powerRune in powerRunes)
        {
            powerRune.GetComponent<GlowTweener>().TurnOffGlow();
            ColorTweener targetTweener = powerRune.GetComponent<ColorTweener>();
            targetTweener.SetImageAlpha(.5f);
        }
    }

    IEnumerator HighlightRune(GameObject targetObject)
    {
        //ResetHightLight();
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetTweener.TriggerAlphaImageTween(1f, 10f);
        yield return new WaitForSeconds(.1f);
        targetGlow.SetGlowColor(Color.blue);
        targetGlow.TriggerGlowTween(12f, 15f);
        print($"{targetObject.name} is highlighted");
    }

    IEnumerator SetRotation()
    {
        ResetHightLight();
        hitInerruption = false;

        //float delayModifier = 4f;
        SetTargetRotation();
        transform.Rotate(0, 0, targetRotaton);
        for (float timer = triggerTimeLimit; timer >= 0; timer -= Time.deltaTime)
        {
            if (hitInerruption)
            {
                UnHighlightRune(targetRune);
                //ResetHightLight();
                print("hit interrupt event");
                hitInerruption = false;
                StartCoroutine(SetRotation());
                SetTargetRotation();
                transform.Rotate(0, 0, targetRotaton);
                //timer = triggerTimeLimit;
                yield break;
            }
            yield return null;
        }
        if (!hitInerruption)
        {
            print("normal rotation coroutine end");
            StartCoroutine(SetRotation());
        }
        // TODO: revisit this logic for faster switching to next rune highlight
        //if (hitCount == 1)
        //{
        //    yield return new WaitForSeconds(triggerTimeLimit / delayModifier);
        //    //hitInerruption = true;
        //}
        //else if (hitCount >= 2)
        //{
        //    yield return new WaitForSeconds(triggerTimeLimit / delayModifier);
        //    //hitInerruption = true;
        //    yield return new WaitForSeconds(triggerTimeLimit / delayModifier);
        //    //hitInerruption = true;
        //}
    }
    public void StartRotation()
    {
        StartCoroutine(SetRotation());
    }
    public void StopRotation()
    {
        transform.Rotate(0, 0, 45f);
        StopAllCoroutines();
    }

    void ResetSuccessHightlight()
    {
        foreach (GameObject powerRune in powerRunes)
        {
            GameObject targetHighlight = powerRune.transform.GetChild(0).gameObject;
            ScaleTweener scaleTweener = targetHighlight.GetComponent<ScaleTweener>();
            ColorTweener colorTweener = targetHighlight.GetComponent<ColorTweener>();
            GlowTweener glowTweener = targetHighlight.GetComponent<GlowTweener>();
            colorTweener?.SetImageAlpha(0);
            scaleTweener?.SetUniformScale(1f);
            glowTweener?.TriggerGlowByDuration(1f, 0);
        }

    }

    IEnumerator TriggerSuccessHighlight()
    {
        float tweenDuration = .5f;
        GameObject targetHighlight = targetRune.transform.GetChild(0).gameObject;
        ScaleTweener scaleTweener = targetHighlight.GetComponent<ScaleTweener>();
        ColorTweener colorTweener = targetHighlight.GetComponent<ColorTweener>();
        GlowTweener glowTweener = targetHighlight.GetComponent<GlowTweener>();
        colorTweener?.TriggerImageAlphaByDuration(.6f, tweenDuration);
        scaleTweener?.TriggerUniformScaleTween(2f, tweenDuration * 2);
        glowTweener?.SetGlowColor(Color.yellow);
        glowTweener?.TriggerGlowByDuration(5f, tweenDuration);
        yield return new WaitForSeconds(tweenDuration);
        glowTweener?.TriggerGlowByDuration(1f, tweenDuration / 4);
        colorTweener?.TriggerImageAlphaByDuration(0, tweenDuration * 4);
        scaleTweener?.TriggerUniformScaleTween(2.25f, tweenDuration * 2);
        yield return new WaitForSeconds(tweenDuration * 2);
        scaleTweener?.SetUniformScale(1f);
    }

    void ShowHitSuccess()
    {
        hitAttamptCount++;
        StartCoroutine(TriggerSuccessHighlight());

        if (hitAttamptCount < 2)
        {
            runeAnimationSoundFX.PlayHitSuccess();
        }
    }

    void RevealOrbitRing()
    {
        int targetIndex = hitCount - 1;
        GameObject targetRing = orbitRings[targetIndex];
        GameObject targetDot = orbitDots[targetIndex];
        ColorTweener targetRingColor = targetRing.GetComponent<ColorTweener>();
        UtilityScaleTweener targetRingScaler = targetRing.GetComponent<UtilityScaleTweener>();
        ColorTweener targetDotColor = targetDot.GetComponent<ColorTweener>();
        targetRingColor.TriggerAlphaImageTween(1f, 3f);
        targetRingScaler.TriggerScale(1f, 3f);
        targetDotColor.TriggerAlphaImageTween(1f, 3f);
        targetDot.GetComponent<GlowTweener>().TriggerGlowTween(7f);
        targetRing.GetComponent<GlowTweener>().TriggerGlowTween(7f);

        if (targetIndex == 0)
        {
            runeAnimationSoundFX.PlayRuneHit1();
        }
        else if (targetIndex == 1)
        {
            runeAnimationSoundFX.PlayRuneHit2();
        }
        else if (targetIndex == 2)
        {
            runeAnimationSoundFX.PlayRuneHit3();
            runeIntroSequencer.winTrigger = true;
        }
    }

    void HideOrbitRing(int targetIndex)
    {
        //int targetIndex = hitCount - 1;
        if (!success)
        {
            runeAnimationSoundFX.PlayRuneMiss();
        }
        GameObject targetRing = orbitRings[targetIndex];
        GameObject targetDot = orbitDots[targetIndex];
        ColorTweener targetRingColor = targetRing.GetComponent<ColorTweener>();
        UtilityScaleTweener targetRingScaler = targetRing.GetComponent<UtilityScaleTweener>();
        ColorTweener targetDotColor = targetDot.GetComponent<ColorTweener>();
        float targetScale = orbitScales[targetIndex];

        targetDot.GetComponent<GlowTweener>().TriggerGlowTween(0, 4f);
        targetRing.GetComponent<GlowTweener>().TriggerGlowTween(0, 4f);

        targetRingColor.TriggerAlphaImageTween(0, 3f);
        targetRingScaler.TriggerScale(targetScale, 3f);
        targetDotColor.TriggerAlphaImageTween(0, 3f);

    }

    IEnumerator ResetOrbitRings()
    {
        yield return new WaitForSeconds(2f);
        int targetIndex = 0;
        foreach (GameObject orbitRing in orbitRings)
        {
            HideOrbitRing(targetIndex);
            targetIndex++;
        }
        success = false;
    }

    public void TriggerOrbitRingReset()
    {
        StartCoroutine(ResetOrbitRings());
    }

    public IEnumerator TriggerFailure()
    {
        yield return new WaitForSeconds(timeLimit);
        if (hitCount < hitSuccessLimit)
        {
            failure = true;
        }
    }

    public void ResetHitCount()
    {
        hitCount = 0;
        print($"hitcount: {hitCount}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !runeIntroSequencer.exitAnimationStarted && runeIntroSequencer.inputActive)
        {
            if (hitActive)
            {
                if (hitCount < hitSuccessLimit)
                {
                    hitCount++;
                    RevealOrbitRing();
                    hitInerruption = true;
                    ShowHitSuccess();
                    //ResetHightLight();
                }

                if (hitCount >= hitSuccessLimit && !failure)
                {
                    success = true;
                }
            }
            else
            {
                if (hitCount > 0)
                {
                    HideOrbitRing(hitCount - 1);
                    hitCount--;
                }
            }
        }

        if (hitCount >= hitSuccessLimit && !failure)
        {
            success = true;
        }
    }
}
