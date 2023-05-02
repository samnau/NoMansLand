using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class RadarSweeperTargetController : BattleChallenge
{
    float rotationMultiplier;
    float rotationModfier = 1f;
    float rotationBase = 90f;
    float targetRotaton;
    public GameObject battleTrigger;
    int hitCount = 0;
    bool hitActive = false;
    public int hitSuccessLimit = 4;
    public float triggerTimeLimit = 2f;
    //ColorTweener colorTweener;
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


    GameObject[] orbitRings;
    GameObject[] orbitDots;
    float[] orbitScales;

    [SerializeField]
    GameObject pointerArm;

    bool hitInerruption = false;
    bool stopRotation = false;

    RuneAnimationSoundFX runeAnimationSoundFX;

    RuneIntroSequencer runeIntroSequencer;

    void Start()
    {
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;

        orbitRings = new GameObject[] { orbitRing1, orbitRing2, orbitRing3 };
        orbitDots = new GameObject[] { orbitDot1, orbitDot2, orbitDot3 };

        orbitRing1Scale = orbitRing1.transform.localScale.y;
        orbitRing2Scale = orbitRing2.transform.localScale.y;
        orbitRing3Scale = orbitRing3.transform.localScale.y;

        orbitScales = new float[] { orbitRing1Scale, orbitRing2Scale, orbitRing3Scale };
        runeAnimationSoundFX = FindObjectOfType<RuneAnimationSoundFX>();
        runeIntroSequencer = FindObjectOfType<RuneIntroSequencer>();

        SetTargetRotation();
    }

    void SetTargetRotation()
    {
        var randomModfier = Random.Range(0, 10);
        rotationMultiplier = Random.Range(1, 4) * 1f;
        rotationModfier = randomModfier < 5 ? -1f : 1f;
        targetRotaton = rotationBase * rotationMultiplier * rotationModfier;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == battleTrigger.name)
        {
            hitActive = true;
        }

        if (collision.CompareTag("BattleTrigger"))
        {
            ColorTweener targetTweener = collision.gameObject.GetComponent<ColorTweener>();
            targetTweener.TriggerAlphaImageTween(1f, 10);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == battleTrigger.name)
        {
            hitActive = false;
        }
        if (collision.CompareTag("BattleTrigger"))
        {
            ColorTweener targetTweener = collision.gameObject.GetComponent<ColorTweener>();
            targetTweener.TriggerAlphaImageTween(0.5f, 10);
        }
    }
    IEnumerator SetRotation()
    {
        float delayModifier = 4f;
        SetTargetRotation();
        transform.Rotate(0, 0, targetRotaton);
        for (float timer = triggerTimeLimit; timer >= 0; timer -= Time.deltaTime)
        {
            if (hitInerruption)
            {
                hitInerruption = false;
                StartCoroutine(SetRotation());
                yield break;
            }
            yield return null;
        }
        if(!hitInerruption)
        {
            StartCoroutine(SetRotation());
        }

        if (hitCount == 1)
        {
            yield return new WaitForSeconds(triggerTimeLimit / delayModifier);
            hitInerruption = true;
        }
        else if (hitCount >= 2)
        {
            yield return new WaitForSeconds(triggerTimeLimit / delayModifier);
            hitInerruption = true;
            yield return new WaitForSeconds(triggerTimeLimit / delayModifier);
            hitInerruption = true;
        }
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

        if(targetIndex == 0)
        {
            runeAnimationSoundFX.PlayRuneHit1();
        } else if (targetIndex == 1)
        {
            runeAnimationSoundFX.PlayRuneHit2();
        } else if (targetIndex == 2)
        {
            runeAnimationSoundFX.PlayRuneHit3();
            runeIntroSequencer.winTrigger = true;
        }
    }

    void HideOrbitRing()
    {
        int targetIndex = hitCount - 1;
        runeAnimationSoundFX.PlayRuneMiss();
        GameObject targetRing = orbitRings[targetIndex];
        GameObject targetDot = orbitDots[targetIndex];
        ColorTweener targetRingColor = targetRing.GetComponent<ColorTweener>();
        UtilityScaleTweener targetRingScaler = targetRing.GetComponent<UtilityScaleTweener>();
        ColorTweener targetDotColor = targetDot.GetComponent<ColorTweener>();
        float targetScale = orbitScales[targetIndex];
        targetRingColor.TriggerAlphaImageTween(0, 3f);
        targetRingScaler.TriggerScale(targetScale, 3f);
        targetDotColor.TriggerAlphaImageTween(0, 3f);
    }

    public IEnumerator TriggerFailure()
    {
        yield return new WaitForSeconds(timeLimit);
        if (hitCount < hitSuccessLimit)
        {
            print("YOU FAILED...");
            failure = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && !runeIntroSequencer.exitAnimationStarted)
        {
            if(hitActive)
            {
                if (hitCount < hitSuccessLimit)
                {
                    hitCount++;
                    RevealOrbitRing();
                    hitInerruption = true;
                }

                if (hitCount >= hitSuccessLimit && !failure)
                {
                    success = true;
                }
            } else
            {
                print("miss");
                if(hitCount > 0)
                {
                    HideOrbitRing();
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
