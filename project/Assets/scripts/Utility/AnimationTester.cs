using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    ColorTweener colorTweener;
    GlowTweener glowTweener;
    // Start is called before the first frame update
    void Start()
    {
        colorTweener = GetComponent<ColorTweener>();
        glowTweener = GetComponent<GlowTweener>();
        StartCoroutine(AnimationSequence1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AnimationSequence1()
    {
        yield return new WaitForSeconds(.5f);
        colorTweener.TriggerAlphaSpriteTween(1f, 10f);
        yield return new WaitForSeconds(.1f);
        glowTweener.TriggerGlowTween(7f, 4f);

        yield return new WaitForSeconds(3f);
        glowTweener.TriggerGlowTween(0f, 4f);
        yield return new WaitForSeconds(.1f);
        colorTweener.TriggerAlphaSpriteTween(0.5f, 6f);
    }
}
