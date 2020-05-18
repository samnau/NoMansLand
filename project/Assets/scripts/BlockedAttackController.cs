using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedAttackController : MonoBehaviour {
    public bool attackBlocked = false;
    GameObject head;
    Animator headAnimator;
    GameObject beak;
    Animator beakAnimator;
	// Use this for initialization
	void Start () {
        head = gameObject.transform.Find("beakWrapper").gameObject;
        headAnimator = head.GetComponent<Animator>();
        beak = gameObject.transform.Find("raven-spider_head").gameObject;
        beakAnimator = beak.GetComponent<Animator>();
	}
	
    IEnumerator TriggerRecoil()
    {
        headAnimator.SetBool("recoil", true);
        beakAnimator.SetBool("recoil", true);
        yield return new WaitForSeconds(0.1f);
        attackBlocked = false;
    } 

	// Update is called once per frame
	void Update () {
		if(attackBlocked)
        {
            StartCoroutine(TriggerRecoil());
        }
	}
}
