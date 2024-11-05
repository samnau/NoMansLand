using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerCharger : BattleChallenge
{
    float[] challengeRotations;
    float startRotation;
    float currentRotation = 0f;
    float minRotation = 0f;
    float maxRotation = 450f;
    float chargeIncrement = 1f;
    public bool inputActive = false;
    public bool challengeActive = false;
    [SerializeField]
    GameObject triggerWrapper;
    RotationTweener triggerRotator;
    float[] targetRotations;
    GameObject pointer;
    GameObject pointerArm;
    float pointerAlpha;
    float pointerGlow;
    float pointerArmAlpha;
    float pointerArmGlow;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = currentRotation = transform.localEulerAngles.z;
        FindBattleIndicators();
        //FindTriggerWrapper();
        runeAnimationSoundFX = gameObject.GetComponentInParent<RuneAnimationSoundFX>();
        targetRotations = new float[]{ -35f, -145f, -220f };
        triggerRotator = triggerWrapper?.GetComponent<RotationTweener>();
        //StartCoroutine(RotateTriggerWrapper());
        pointer = this.gameObject;
        pointerArm = pointer.transform.GetChild(0).gameObject;

        SetPointerColors();
    }

    public void SetPointerColors()
    {
        pointerAlpha = pointer.GetComponent<Image>().color.a;
        pointerGlow = pointer.GetComponent<Image>().material.GetFloat("_Fade");
        pointerArmAlpha = pointerArm.GetComponent<Image>().color.a;
        pointerArmGlow = pointerArm.GetComponent<Image>().material.GetFloat("_Fade");
    }

    public bool SuccessLimitReached()
    {
        return hitCount >= hitSuccessLimit;
    }

    // NOTE: candidate for refactor?
    IEnumerator TriggerSuccessHighlight()
    {
        runeAnimationSoundFX.PlayHitSuccess();
        float tweenDuration = .5f;
        GameObject targetHighlight = triggerWrapper.GetComponentInChildren<ScaleTweener>().gameObject;
        ScaleTweener scaleTweener = targetHighlight.GetComponent<ScaleTweener>();
        ColorTweener colorTweener = targetHighlight.GetComponent<ColorTweener>();
        GlowTweener glowTweener = targetHighlight.GetComponent<GlowTweener>();
        colorTweener?.TriggerImageAlphaByDuration(1f, tweenDuration);
        scaleTweener?.TriggerUniformScaleTween(1.5f, tweenDuration * 2);
        glowTweener?.SetGlowColor(Color.yellow);
        glowTweener?.TriggerGlowByDuration(5f, tweenDuration);
        yield return new WaitForSeconds(tweenDuration * 1);
        ResetSuccessHightlight();
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

    void TriggerPowerCharge()
    {
        bool isBelowMaxRotation = currentRotation > 60f || currentRotation == 0;
        if (Input.GetKeyDown(KeyCode.Space) && isBelowMaxRotation && inputActive)
        {
            transform.Rotate(0, 0, -1100f * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

    void HighlightPointer()
    {
        if (!inputActive)
        {
            return;
        }
        pointer.GetComponent<GlowTweener>().TriggerGlowByDuration(pointerGlow, .3f);
        pointer.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(pointerAlpha, .3f);
        pointerArm.GetComponent<GlowTweener>().TriggerGlowByDuration(pointerArmGlow, .3f);
        pointerArm.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(pointerArmAlpha, .3f);
    }
    void UnhighlightPoiner()
    {
        SetPointerColors();

        pointer.GetComponent<GlowTweener>().TriggerGlowByDuration(0, .3f);
        pointer.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(.5f, .3f);
        pointerArm.GetComponent<GlowTweener>().TriggerGlowByDuration(0, .3f);
        pointerArm.GetComponent<ColorTweener>().TriggerImageAlphaByDuration(.5f, .3f);
    }

    public IEnumerator RotateTriggerWrapper()
    {
        inputActive = false;
        if (hitCount >= hitSuccessLimit)
        {
            yield break;
        }
        float rotationDuration = 1f;
        triggerRotator?.TriggerRotation(targetRotations[hitCount], rotationDuration);
        yield return new WaitForSeconds(rotationDuration);
        if(!SuccessLimitReached())
        {
            inputActive = true;
            HighlightPointer();
        }
    }

    void ReducePower()
    {
        currentRotation = transform.localEulerAngles.z;
        bool isWithinResetMargin = currentRotation > 359.5f || currentRotation < 70f;
        if (currentRotation > 0 && !isWithinResetMargin)
        {
            transform.Rotate(0, 0, 50f * Time.deltaTime);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isBattleTrigger = collision.CompareTag("BattleTrigger");
        if(isBattleTrigger && inputActive && !success && !failure)
        {
            hitCount++;
            StartCoroutine(TriggerSuccessHighlight());
            StartCoroutine(RotateTriggerWrapper());
            RevealOrbitRing();
            UnhighlightPoiner();
            success = SuccessLimitReached();
        }

        if(isBattleTrigger && success)
        {
            triggerRotator?.TriggerRotation(45f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inputActive && !success && !failure)
        {
            TriggerPowerCharge();
        }
        if(challengeActive)
        {
            ReducePower();
        }
    }
}
