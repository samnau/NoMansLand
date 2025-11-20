using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRingTrigger : BattleChallenge
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
            spinnerSoundFX?.PlayHitSuccess(.2f);
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
        }
    }

    protected override IEnumerator HighlightRune(GameObject targetObject)
    {
        //GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetTweener.TriggerImageAlphaByDuration(1f, .1f);
        yield return new WaitForSeconds(.15f);
        targetTweener.TriggerImageAlphaByDuration(.5f, .1f);
    }
}
