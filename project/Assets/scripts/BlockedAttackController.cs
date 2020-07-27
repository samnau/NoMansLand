using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedAttackController : MonoBehaviour {
    public bool attackBlocked = false;
    GameObject hero;
    Transform head;
    Animator headAnimator;
    Transform beak;
    Animator beakAnimator;
    Transform MonsterWrapper;
	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        MonsterWrapper = gameObject.transform.Find("RavenSpiderWrapper");
        head = MonsterWrapper.Find("raven-spider_head");
        headAnimator = head.GetComponent<Animator>();
        beak = head.Find("beakWrapper"); ;
        beakAnimator = beak.GetComponent<Animator>();
	}
	
    IEnumerator TriggerRecoil()
    {
        Debug.Log("recoil");

        headAnimator.SetBool("recoil", true);
        beakAnimator.SetBool("recoil", true);
        TriggerLegsRecoil();
        yield return new WaitForSeconds(0.1f);
        attackBlocked = false;
        yield return new WaitForSeconds(1.5f);
        MonsterWrapper.GetComponent<Animator>().SetBool("retreat", true);
    } 

    void TriggerLegsRecoil()
    {
        var animationArray = MonsterWrapper.GetComponentsInChildren<Animator>();
        foreach (Animator targetAnimation in animationArray)
        {
            targetAnimation.SetBool("stabBlocked", true);
        }
    }

	// Update is called once per frame
	void Update () {
		if(attackBlocked)
        {
            StartCoroutine(TriggerRecoil());
        }
	}
}
