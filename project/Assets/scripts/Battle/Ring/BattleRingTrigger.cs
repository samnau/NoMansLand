using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRingTrigger : MonoBehaviour
{
    BattleSpinner battleSpinner;
    public bool inputActive = false;
    RuneAnimationSoundFX spinnerSoundFX;

    void Start()
    {
        battleSpinner = GetComponent<BattleSpinner>();

        spinnerSoundFX = GetComponentInParent<RuneAnimationSoundFX>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!inputActive)
        {
            return;
        }
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger && !battleSpinner.SuccessLimitReached())
        {
            battleSpinner.triggerValid = true;
            StartCoroutine(HighlightRune(collision.gameObject));
            spinnerSoundFX?.PlayHitSuccess(.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!inputActive)
        {
            return;
        }

        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger && !battleSpinner.SuccessLimitReached())
        {
            battleSpinner.triggerValid = false;
            //StartCoroutine(UnHighlightRune(collision.gameObject));
        }
    }

    IEnumerator UnHighlightRune(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();

        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        yield return new WaitForSeconds(.1f);
        targetTweener.TriggerAlphaImageTween(0.5f, 40f);
    }

    IEnumerator HighlightRune(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        //targetTweener.TriggerAlphaImageTween(1f, 20f);
        targetTweener.TriggerImageAlphaByDuration(1f, .1f);
        yield return new WaitForSeconds(.15f);
        targetTweener.TriggerImageAlphaByDuration(.5f, .1f);
    }
}
