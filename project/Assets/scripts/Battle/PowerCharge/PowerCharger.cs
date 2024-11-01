using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCharger : BattleChallenge
{
    float[] challengeRotations;
    float startRotation;
    float currentRotation = 0f;
    float minRotation = 0f;
    float maxRotation = 450f;
    float chargeIncrement = 1f;
    public bool inputActive = true;
    [SerializeField]
    GameObject triggerWrapper;
    RotationTweener triggerRotator;
    float[] targetRotations;
    GameObject pointer;
    GameObject pointerArm;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = currentRotation = transform.localEulerAngles.z;
        FindBattleIndicators();
        //FindTriggerWrapper();
        runeAnimationSoundFX = gameObject.GetComponentInParent<RuneAnimationSoundFX>();
        targetRotations = new float[]{ -35f, -145f, -200f };
        triggerRotator = triggerWrapper?.GetComponent<RotationTweener>();
        StartCoroutine(RotateTriggerWrapper());
        pointer = this.gameObject;
        pointerArm = pointer.transform.GetChild(0).gameObject;
        //print(triggerWrapper.gameObject.name);
        //triggerWrapper.GetComponent<RotationTweener>().TriggerRotation(targetRotations[hitCount], 400f);
    }

    // NOTE: candidate for refactor?
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
        bool isBelowMaxRotation = currentRotation > 90f || currentRotation == 0;
        if (Input.GetKeyDown(KeyCode.Space) && isBelowMaxRotation && inputActive)
        {
            transform.Rotate(0, 0, -1000f * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

    //void FindTriggerWrapper()
    //{
    //    GameObject targetParent = this.GetComponentInParent<PowerIntroSequencer>().gameObject;
    //    for (int i = 0; i < targetParent.transform.childCount; i++)
    //    {
    //        if (targetParent.transform.GetChild(i).CompareTag("BattleTrigger"))
    //        {
    //            triggerWrapper = targetParent.transform.GetChild(i).parent.gameObject;
    //        }
    //        print(targetParent.transform.GetChild(i).name);

    //    }

    //}

    void HighlightPointer()
    {

    }
    void UnhighlightPoiner()
    {
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
        inputActive = true;
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
        if(isBattleTrigger && inputActive)
        {
            hitCount++;
            StartCoroutine(TriggerSuccessHighlight());
            StartCoroutine(RotateTriggerWrapper());
            RevealOrbitRing();
            UnhighlightPoiner();
        }
    }

    // Update is called once per frame
    void Update()
    {
        TriggerPowerCharge();
        ReducePower();
    }
}
