using System.Collections;
using System.Collections.Generic;
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
    public float triggerTimeLimit = .75f;
    ColorTweener colorTweener;
    [SerializeField]
    GameObject orbitRing1;
    [SerializeField]
    GameObject orbitRing2;
    [SerializeField]
    GameObject orbitRing3;
    GameObject orbitDot1;
    GameObject orbitDot2;
    GameObject orbitDot3;

    GameObject[] orbitRings;
    GameObject[] orbitDots;

    [SerializeField]
    GameObject pointerArm;

    void Start()
    {
        orbitDot1 = orbitRing1.transform.Find("orbit ring 1 dot").gameObject;
        orbitDot2 = orbitRing2.transform.Find("orbit ring 2 dot").gameObject;
        orbitDot3 = orbitRing3.transform.Find("orbit ring 3 dot").gameObject;

        orbitRings = new GameObject[] { orbitRing1, orbitRing2, orbitRing3 };
        orbitDots = new GameObject[] { orbitDot1, orbitDot2, orbitDot3 };

        SetTargetRotation();
        StartCoroutine(SetRotation());
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
            print("sweeper trigger");
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
            print("sweeper trigger");
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
        yield return new WaitForSeconds(triggerTimeLimit);
        SetTargetRotation();
        transform.Rotate(0, 0, targetRotaton);
        yield return new WaitForSeconds(triggerTimeLimit);
        StartCoroutine(SetRotation());
    }

    void RevealOrbitRing()
    {
        print($"ring: {orbitRings[hitCount]}");
        GameObject targetRing = orbitRings[hitCount];
        GameObject targetDot = orbitDots[hitCount];
        ColorTweener targetRingColor = targetRing.GetComponent<ColorTweener>();
        UtilityScaleTweener targetRingScaler = targetRing.GetComponent<UtilityScaleTweener>();
        ColorTweener targetDotColor = targetDot.GetComponent<ColorTweener>();
        targetRingColor.TriggerAlphaImageTween(1f, 3f);
        targetRingScaler.TriggerScale(1f, 3f);
        targetDotColor.TriggerAlphaImageTween(1f, 3f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && hitActive)
        {
            if (hitCount < hitSuccessLimit)
            {
                RevealOrbitRing();
            }

            hitCount++;

            print($"hit: {hitCount}");

            if (hitCount >= hitSuccessLimit)
            {
                print("YOU WIN!!!");
            }
        }
    }
}
