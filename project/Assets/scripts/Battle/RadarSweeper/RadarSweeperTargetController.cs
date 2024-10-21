using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RadarSweeperTargetController : BattleChallenge
{
    float rotationMultiplier;
    float rotationModfier = 1f;
    float rotationBase = 90f;
    float targetRotaton;
    public GameObject battleTrigger;
    public bool hitActive = false;
    public float triggerTimeLimit = 2f;

    GameObject[] powerRunes;

    GameObject targetRune;

    [SerializeField]
    GameObject pointerArm;

    bool hitInerruption = false;
    bool stopRotation = false;
    bool hitAttempted = false;
    [SerializeField] bool isRuneSpinner = false;

    int hitAttamptCount = 0;

    [SerializeField] GameEvent cameraShake;

    RuneIntroSequencer runeIntroSequencer;

    void Start()
    {
        runeIntroSequencer = gameObject.GetComponentInParent<RuneIntroSequencer>();

        FindBattleIndicators();

        powerRunes = runeIntroSequencer.powerRunes;

        runeAnimationSoundFX = gameObject.GetComponentInParent<RuneAnimationSoundFX>();

        SetTargetRotation();
    }

    // NOTE: this method only is used to set the rotation after the first rotaction of the target is finished
    void SetTargetRotation()
    {
        var randomModfier = Random.Range(0, 10);
        rotationMultiplier = Random.Range(1, 4) * 1f;
        rotationModfier = randomModfier < 5 ? -1f : 1f;

        targetRotaton = rotationBase * rotationMultiplier * rotationModfier;
    }

    //NOTE: potential refactor
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

    //NOTE: potential refactor
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
        }
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

    public float SetPointerTriggerStartRotation()
    {
        var randomModfier = Random.Range(0, 10);
        float rotationMultiplier = Random.Range(1, 4) * 1f;
        float rotationModfier = randomModfier < 5 ? -1f : 1f;
        return isRuneSpinner ? 90f : 90f * rotationMultiplier * rotationModfier;
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
        if(!hitInerruption)
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
        float tweenDuration = .25f;
        GameObject targetHighlight = targetRune.transform.GetChild(0).gameObject;
        ScaleTweener scaleTweener = targetHighlight.GetComponent<ScaleTweener>();
        ColorTweener colorTweener = targetHighlight.GetComponent<ColorTweener>();
        GlowTweener glowTweener = targetHighlight.GetComponent<GlowTweener>();
        colorTweener?.TriggerImageAlphaByDuration(.6f, tweenDuration);
        scaleTweener?.TriggerUniformScaleTween(2f, tweenDuration);
        glowTweener?.SetGlowColor(Color.yellow);
        glowTweener?.TriggerGlowByDuration(5f, tweenDuration);
        yield return new WaitForSeconds(tweenDuration);
        glowTweener?.TriggerGlowByDuration(0f, tweenDuration/4);
        colorTweener?.TriggerImageAlphaByDuration(0, tweenDuration*2);
        scaleTweener?.TriggerUniformScaleTween(2.25f, tweenDuration*2);
        yield return new WaitForSeconds(tweenDuration);
        scaleTweener?.SetUniformScale(1f);
    }

    void ShowHitSuccess()
    {
        hitAttamptCount++;
        StartCoroutine(TriggerSuccessHighlight());
        
        if (hitAttamptCount <2)
        {
            runeAnimationSoundFX.PlayHitSuccess();
        }
    }
    protected override void RevealOrbitRing()
    {
        base.RevealOrbitRing();
        int targetIndex = hitCount > 0 ? hitCount - 1 : 0;
        if (targetIndex == 2)
        {
            runeIntroSequencer.winTrigger = true;
        }
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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !runeIntroSequencer.exitAnimationStarted && runeIntroSequencer.inputActive)
        {
            if(hitActive)
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
            } else
            {
                if(hitCount > 0)
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
