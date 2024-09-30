using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpinner : BattleChallenge
{
    Transform targetTransform;
    float rotationModifier = 1.0f;
    public bool rotationActive = true;
    public float rotationSpeed = 400f;
    float defaultRotationSpeed;
    int successCount = 0;
    public int successLimit = 3;
    [HideInInspector]
    public bool triggerValid = false;
    [SerializeField]
    KeyCode triggerKey = KeyCode.Space;
    GameObject battleTrigger;
    [SerializeField] GameObject triggerWrapper;
    RotationTweener rotationTweener;

    GameObject[] orbitRings;
    GameObject[] orbitDots;
    float[] orbitScales;

    RuneAnimationSoundFX runeAnimationSoundFX;

    void Start()
    {
        targetTransform = gameObject.transform;
        battleTrigger = GameObject.FindGameObjectWithTag("BattleTrigger");
        //triggerWrapper =  battleTrigger.transform.parent.gameObject;
        rotationTweener = triggerWrapper.GetComponent<RotationTweener>();
        defaultRotationSpeed = rotationSpeed;

        print(GameObject.FindGameObjectsWithTag("BattleIndicator").Length);
        orbitRings = GameObject.FindGameObjectsWithTag("BattleIndicator");
        orbitDots = new GameObject[3];
        orbitScales = new float[3];
        for (int i = 0; i <= orbitRings.Length-1; i++)
        {
            print(orbitRings[i].transform.GetChild(0).gameObject);
            orbitDots[i] = orbitRings[i]?.transform?.GetChild(0)?.gameObject;
            orbitScales[i] = orbitRings[i].transform.localScale.y;
        }

        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();

        // DEBUGGING: disabling the time limit for testing
        StartCoroutine(Timeout());
    }
    bool IsKeyValid()
    {
        return Input.GetKeyDown(triggerKey);
    }
    public void ReverseRotation()
    {
        rotationModifier = rotationModifier * -1f;
    }
    public bool SuccessLimitReached()
    {
        return successCount >= successLimit;
    }

    void ResetSuccessHightlight()
    {
        GameObject targetHighlight = triggerWrapper.GetComponentInChildren<ScaleTweener>().gameObject;
        ScaleTweener scaleTweener = targetHighlight.GetComponent<ScaleTweener>();
        ColorTweener colorTweener = targetHighlight.GetComponent<ColorTweener>();
        GlowTweener glowTweener = targetHighlight.GetComponent<GlowTweener>();
        colorTweener?.SetImageAlpha(0);
        scaleTweener?.SetUniformScale(1f);
        glowTweener?.TriggerGlowByDuration(1f, 0);

    }

    IEnumerator TriggerSuccessHighlight()
    {
        float tweenDuration = .5f;
        GameObject targetHighlight = triggerWrapper.GetComponentInChildren<ScaleTweener>().gameObject;
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
        scaleTweener?.TriggerUniformScaleTween(2f, tweenDuration * 2);
        yield return new WaitForSeconds(tweenDuration * 1.2f);
        ResetSuccessHightlight();
    }

    void RotateTriggerWrapper()
    {
        float targetRotation = Random.Range(60f, 270f);
        rotationTweener?.TriggerRotation(targetRotation);
        float targetRuneRotation = battleTrigger.transform.rotation.z + (targetRotation * -1f);
        RotationTweener runeRotator = battleTrigger.GetComponent<RotationTweener>();
        triggerWrapper?.GetComponent<RotationTweener>().TriggerRotation(targetRuneRotation);
    }

    void RevealOrbitRing()
    {
        int targetIndex = successCount - 1;
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
            // NOTE: use this in hte new sequencer to trigger the win condition
            //runeIntroSequencer.winTrigger = true;
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

    void CheckForValidTrigger()
    {
        print($"triggervalid: {triggerValid}");
        if (triggerValid && !SuccessLimitReached())
        {
            successCount++;
            RotateTriggerWrapper();
            ReverseRotation();
            RevealOrbitRing();
            StartCoroutine(TriggerSuccessHighlight());
            rotationSpeed += 75f;
            runeAnimationSoundFX.PlayHitSuccess();

            if (SuccessLimitReached())
            {
                rotationSpeed += 175f;
                success = true;
            }

        }
        else
        {
            if(successCount > 0)
            {
                HideOrbitRing(successCount-1);
                rotationSpeed -= 75f;
                successCount -= 1;
            } else
            {
                HideOrbitRing(successCount);
            }
        }
    }
    public void SetRotation()
    {
        if (!rotationActive)
        {
            return;
        }
        var targetRotation = rotationSpeed * Time.deltaTime * rotationModifier;
        transform.Rotate(0, 0, targetRotation);
    }

    void Update()
    {
        SetRotation();
        if (IsKeyValid() && !success && !failure)
        {
            print("valid trigger key?");

            CheckForValidTrigger();
        }
    }
}
