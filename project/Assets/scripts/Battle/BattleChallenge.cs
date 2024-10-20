using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChallenge : MonoBehaviour
{
    [HideInInspector]
    public bool success = false;
    [HideInInspector]
    public bool failure = false;
    public float timeLimit = 10f;
    [HideInInspector] public int hitCount = 0;

    protected GameObject[] orbitRings;
    protected GameObject[] orbitDots;
    protected float[] orbitScales;

    protected RuneAnimationSoundFX runeAnimationSoundFX;

    public IEnumerator Timeout()
    {
        print("timer started");
        yield return new WaitForSeconds(timeLimit);
        if (!success)
        {
            print("time's up!");
            failure = true;
        }
    }
    protected virtual void RevealOrbitRing()
    {
        int targetIndex = hitCount > 0 ? hitCount - 1 : 0;
        print($"hit count is: {hitCount}");
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
        var targetSequencer = FindParentWithTag(this.gameObject, "BattleChallenge");

        // REFACTOR: this is repeated code in all challenges
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
            // NOTE: use this in the new sequencer to trigger the win condition
            //targetSequencer.winTrigger = true;
        }
    }

    protected void HideOrbitRing(int targetIndex)
    {
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
    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }
    protected void FindBattleIndicators()
    {
        List<GameObject> tempOrbitRingsList = new List<GameObject>();

        orbitDots = new GameObject[3];
        orbitScales = new float[3];

        var targetSequencer = FindParentWithTag(this.gameObject, "BattleChallenge");

        for (int i = 0; i < targetSequencer.transform.childCount; i++)
        {
            if (targetSequencer.transform.GetChild(i).CompareTag("BattleIndicator"))
            {
                tempOrbitRingsList?.Add(targetSequencer.transform.GetChild(i).gameObject);
            }

        }
        orbitRings = tempOrbitRingsList.ToArray();
        for (int i = 0; i <= orbitRings.Length - 1; i++)
        {
            orbitDots[i] = orbitRings[i]?.transform?.GetChild(0)?.gameObject;
            orbitScales[i] = orbitRings[i].transform.localScale.y;
        }

    }
}
