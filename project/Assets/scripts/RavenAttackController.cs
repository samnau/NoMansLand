using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RavenAttackController : MonoBehaviour {
    GameObject targetLeg;
    Animator legAnimator;
    Animator[] allLegs;
    int[] stabOrder = { 0, 8, 1, 7, 2, 6, 3, 5, 4, 9 };
    int legIndex = 0;
    BattleKeyCombos battleKeyCombos;

	// Use this for initialization
	void Start () {
        targetLeg = GameObject.Find("legNormal_wrapper_1");
        legAnimator = targetLeg.GetComponent<Animator>();
        var frontLegsWrapper = GameObject.Find("frontLegsWrapper");
        var frontLegAnimators = frontLegsWrapper.GetComponentsInChildren<Animator>();
        var backLegsWrapper = GameObject.Find("legDark_wrapper");
        var backLegAnimators = backLegsWrapper.GetComponentsInChildren<Animator>();
        allLegs = frontLegAnimators.Concat(backLegAnimators).ToArray();


        battleKeyCombos = targetLeg.GetComponentInChildren<BattleKeyCombos>();

        //StartCoroutine("TriggerSlash");
    }

    IEnumerable triggerSlash()
    {
        yield return new WaitForSeconds(2.5f);
        legAnimator.SetBool("slash", true);
    }
   IEnumerator ActivateAttack()
    {
        yield return new WaitForSeconds(0.1f);
        //BattleCombos.activeAttack = true;
        battleKeyCombos.activeAttack = true;
    }
    public IEnumerator triggerStab()
    {
        StartCoroutine(ActivateAttack());
        if (legIndex >= stabOrder.Length - 1)
        {
            Debug.Log("stopping the stab");
            StopCoroutine("triggerStab");
            // currentAnimator.SetBool("stab", false);
            //legIndex = 0;
            yield break;
        }
        //Debug.Log("index:" + legIndex);
        var targetStabOrder = stabOrder[legIndex];
        var currentAnimator = allLegs[targetStabOrder];
        //Debug.Log("target order: " + targetStabOrder);
       /* foreach(Animator leg in allLegs)
        {
            var idleController = leg.GetComponentInParent<LegIdleController>();
            //leg.SetBool("startIdle", false);
            idleController.pauseIdle = true;
            leg.SetBool("stab", true);
            yield return null;
            //leg.SetBool("stab", false);
        }*/



        legStab(currentAnimator);
        yield return new WaitForSeconds(0.15f);
        legIndex++;
        currentAnimator.SetBool("stab", false);
        yield return StartCoroutine(triggerStab());
    }

    void legStab(Animator animator)
    {
        animator.SetBool("stab", true);
    }
}
