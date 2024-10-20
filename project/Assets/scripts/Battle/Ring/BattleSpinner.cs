using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpinner : BattleChallenge
{
    Transform targetTransform;
    float rotationModifier = 1.0f;
    public bool rotationActive = false;
    public float rotationSpeed = 400f;
    float defaultRotationSpeed;
    public int successLimit = 3;
    [HideInInspector]
    public bool triggerValid = false;
    [SerializeField]
    KeyCode triggerKey = KeyCode.Space;
    GameObject battleTrigger;
    [SerializeField] GameObject triggerWrapper;
    RotationTweener rotationTweener;
    SpinnerIntroSequencer spinnerIntroSequencer;

    //GameObject[] orbitRings;
    //GameObject[] orbitDots;
    //float[] orbitScales;

    //RuneAnimationSoundFX runeAnimationSoundFX;

    void Start()
    {
        targetTransform = gameObject.transform;
        rotationTweener = triggerWrapper.GetComponent<RotationTweener>();
        spinnerIntroSequencer = gameObject.GetComponentInParent<SpinnerIntroSequencer>();

        defaultRotationSpeed = rotationSpeed;

        //orbitDots = new GameObject[3];
        //orbitScales = new float[3];

        //List<GameObject> tempOrbitRingsList = new List<GameObject>();

        for (int i = 0; i < spinnerIntroSequencer.transform.childCount; i++)
        {
            if (spinnerIntroSequencer.transform.GetChild(i).CompareTag("BattleTrigger"))
            {
                battleTrigger = spinnerIntroSequencer.transform.GetChild(i).gameObject;
            }
        }

        FindBattleIndicators();

        //for (int i = 0; i < spinnerIntroSequencer.transform.childCount; i++)
        //{
        //    if (spinnerIntroSequencer.transform.GetChild(i).CompareTag("BattleIndicator"))
        //    {
        //        tempOrbitRingsList?.Add(spinnerIntroSequencer.transform.GetChild(i).gameObject);
        //    }
        //    if (spinnerIntroSequencer.transform.GetChild(i).CompareTag("BattleTrigger"))
        //    {
        //        battleTrigger = spinnerIntroSequencer.transform.GetChild(i).gameObject;
        //    }
        //}
        //orbitRings = tempOrbitRingsList.ToArray();

        //for (int i = 0; i <= orbitRings.Length-1; i++)
        //{
        //    orbitDots[i] = orbitRings[i]?.transform?.GetChild(0)?.gameObject;
        //    orbitScales[i] = orbitRings[i].transform.localScale.y;
        //}

        runeAnimationSoundFX = gameObject.GetComponentInParent<RuneAnimationSoundFX>();
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
        return hitCount >= successLimit;
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
        colorTweener?.TriggerImageAlphaByDuration(1f, tweenDuration);
        scaleTweener?.TriggerUniformScaleTween(1.5f, tweenDuration * 2);
        glowTweener?.SetGlowColor(Color.yellow);
        glowTweener?.TriggerGlowByDuration(5f, tweenDuration);
        yield return new WaitForSeconds(tweenDuration*1);
        ResetSuccessHightlight();
    }

    void RotateTriggerWrapper()
    {
        // COMEBACK: find a way to ensure that the next rotation amount is not close to the last one
        float targetRotation = Random.Range(60f, 270f);
        rotationTweener?.TriggerRotation(targetRotation);
        float targetRuneRotation = battleTrigger.transform.rotation.z + (targetRotation * -1f);
        triggerWrapper?.GetComponent<RotationTweener>().TriggerRotation(targetRuneRotation);
    }
    protected override void RevealOrbitRing()
    {
        base.RevealOrbitRing();
        int targetIndex = hitCount > 0 ? hitCount - 1 : 0;
        if (targetIndex == 2)
        {
            spinnerIntroSequencer.winTrigger = true;
        }
    }
    //void RevealOrbitRing()
    //{
    //    int targetIndex = hitCount > 0 ? hitCount - 1 : 0;
    //    print($"hit count is: {hitCount}");
    //    GameObject targetRing = orbitRings[targetIndex];
    //    GameObject targetDot = orbitDots[targetIndex];
    //    ColorTweener targetRingColor = targetRing.GetComponent<ColorTweener>();
    //    UtilityScaleTweener targetRingScaler = targetRing.GetComponent<UtilityScaleTweener>();
    //    ColorTweener targetDotColor = targetDot.GetComponent<ColorTweener>();
    //    targetRingColor.TriggerAlphaImageTween(1f, 3f);
    //    targetRingScaler.TriggerScale(1f, 3f);
    //    targetDotColor.TriggerAlphaImageTween(1f, 3f);
    //    targetDot.GetComponent<GlowTweener>().TriggerGlowTween(7f);
    //    targetRing.GetComponent<GlowTweener>().TriggerGlowTween(7f);

    //    // REFACTOR: this is repeated code in all challenges
    //    if (targetIndex == 0)
    //    {
    //        runeAnimationSoundFX.PlayRuneHit1();
    //    }
    //    else if (targetIndex == 1)
    //    {
    //        runeAnimationSoundFX.PlayRuneHit2();
    //    }
    //    else if (targetIndex == 2)
    //    {
    //        runeAnimationSoundFX.PlayRuneHit3();
    //        // NOTE: use this in the new sequencer to trigger the win condition
    //        spinnerIntroSequencer.winTrigger = true;
    //    }

        
    //}

    //void HideOrbitRing(int targetIndex)
    //{
    //    //int targetIndex = hitCount - 1;
    //    if (!success)
    //    {
    //        runeAnimationSoundFX.PlayRuneMiss();
    //    }
    //    GameObject targetRing = orbitRings[targetIndex];
    //    GameObject targetDot = orbitDots[targetIndex];
    //    ColorTweener targetRingColor = targetRing.GetComponent<ColorTweener>();
    //    UtilityScaleTweener targetRingScaler = targetRing.GetComponent<UtilityScaleTweener>();
    //    ColorTweener targetDotColor = targetDot.GetComponent<ColorTweener>();
    //    float targetScale = orbitScales[targetIndex];

    //    targetDot.GetComponent<GlowTweener>().TriggerGlowTween(0, 4f);
    //    targetRing.GetComponent<GlowTweener>().TriggerGlowTween(0, 4f);

    //    targetRingColor.TriggerAlphaImageTween(0, 3f);
    //    targetRingScaler.TriggerScale(targetScale, 3f);
    //    targetDotColor.TriggerAlphaImageTween(0, 3f);
    //}

    //IEnumerator ResetOrbitRings()
    //{
    //    yield return new WaitForSeconds(2f);
    //    int targetIndex = 0;
    //    foreach (GameObject orbitRing in orbitRings)
    //    {
    //        HideOrbitRing(targetIndex);
    //        targetIndex++;
    //    }
    //    success = false;
    //}

    //public void TriggerOrbitRingReset()
    //{
    //    StartCoroutine(ResetOrbitRings());
    //}

    void CheckForValidTrigger()
    {
        if (triggerValid && !SuccessLimitReached())
        {
            hitCount++;
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
            if(hitCount > 0)
            {
                HideOrbitRing(hitCount-1);
                rotationSpeed -= 75f;
                hitCount -= 1;
            } else
            {
                HideOrbitRing(hitCount);
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

    public void EnableRotation()
    {
        rotationActive = true;
    }

    public void DisableRotation()
    {
        rotationActive = false;
    }

    public void ToggleRotation()
    {
        rotationActive = !rotationActive;
    }

    void Update()
    {
        if(rotationActive)
        {
            SetRotation();
        }
        if (IsKeyValid() && !success && !failure)
        {
            CheckForValidTrigger();
        }
    }
}
