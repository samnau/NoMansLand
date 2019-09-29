using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenAttackController : MonoBehaviour {
    GameObject targetLeg;
    Animator legAnimator;

	// Use this for initialization
	void Start () {
        targetLeg = GameObject.Find("legNormal_wrapper_1");
        legAnimator = targetLeg.GetComponent<Animator>();
        StartCoroutine("TriggerSlash");
	}
	
    IEnumerable triggerSlash()
    {
        yield return new WaitForSeconds(2.5f);
        legAnimator.SetBool("slash", true);
    }

}
