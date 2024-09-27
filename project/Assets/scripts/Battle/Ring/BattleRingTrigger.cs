using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRingTrigger : MonoBehaviour
{
    BattleSpinner battleSpiner;

    void Start()
    {
        battleSpiner = FindObjectOfType<BattleSpinner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger)
        {
            battleSpiner.triggerValid = true;
            StartCoroutine(HighlightRune(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger)
        {
            battleSpiner.triggerValid = false;
            StartCoroutine(UnHighlightRune(collision.gameObject));
        }
    }

    IEnumerator UnHighlightRune(GameObject targetObject)
    {
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();

        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        //targetGlow.TurnOffGlow();
        //targetGlow.SetGlowColor(Color.white);

        //targetGlow.TriggerGlowTween(0f, 30f);
        //targetGlow.TurnOffGlow();
        yield return new WaitForSeconds(.1f);
        //targetTweener.SetImageAlpha(.5f);
        targetTweener.TriggerAlphaImageTween(0.5f, 40f);
        //ResetHightLight();
    }

    //void ResetHightLight()
    //{
    //    foreach (GameObject powerRune in powerRunes)
    //    {
    //        powerRune.GetComponent<GlowTweener>().TurnOffGlow();
    //        ColorTweener targetTweener = powerRune.GetComponent<ColorTweener>();
    //        targetTweener.SetImageAlpha(.5f);
    //    }
    //}

    IEnumerator HighlightRune(GameObject targetObject)
    {
        //ResetHightLight();
        GlowTweener targetGlow = targetObject.GetComponent<GlowTweener>();
        ColorTweener targetTweener = targetObject.GetComponent<ColorTweener>();
        targetTweener.TriggerAlphaImageTween(1f, 20f);
        yield return new WaitForSeconds(.1f);
        //targetGlow.SetGlowColor(Color.red);
        //targetGlow.TurnOnGlow();
        //targetGlow.TriggerGlowTween(12f, 30f);
    }
}
