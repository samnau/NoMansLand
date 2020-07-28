using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenStabTrigger : MonoBehaviour {
    Animator frogAnimator;
	// Use this for initialization
	void Start () {
        var targetLeg = GameObject.Find("legNormal_wrapper_1");
        var attackComponent = targetLeg.GetComponentInChildren<BattleKeyCombos>();
        attackComponent.activeAttack = false;
    }

    public void triggerStab()
    {
        var wholeSpider = GameObject.Find("RavenSpiderWrapper");
         //var frog = GameObject.Find("frog_fpo");
         var frog = GameObject.FindGameObjectWithTag("familiar_hitbox");

        StartCoroutine(wholeSpider.GetComponent<RavenAttackController>().triggerStab());
        frogAnimator = frog.GetComponent<Animator>();
        //frogAnimator.SetBool("damage", true);
        StartCoroutine("stopFrogDamage");
    }

    IEnumerator stopFrogDamage()
    {
        yield return new WaitForSeconds(1.5f);
        frogAnimator.SetBool("damage", false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
