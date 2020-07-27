using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RavenAnimationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("raven start");
	}
	
    public void StartLegUnfold ()
    {
        var frontLegsWrapper = GameObject.Find("frontLegsWrapper");
        var frontLegAnimators = frontLegsWrapper.GetComponentsInChildren<Animator>();
        var backLegsWrapper = GameObject.Find("legDark_wrapper");
        var backLegAnimators = backLegsWrapper.GetComponentsInChildren<Animator>();
        var combinedAnimators = frontLegAnimators.Concat(backLegAnimators).ToArray();
        foreach (Animator legAnimator in combinedAnimators)
        {
            legAnimator.SetBool("startUnfold", true);
        }

        var ravenEye = GameObject.Find("raven-spider_eye");
        var ravenEyeAnimator = ravenEye.GetComponent<Animator>();
        ravenEyeAnimator.SetBool("dormant", false);
        var shadow = GameObject.Find("simple_shadow");
        var shadowAnimator = shadow.GetComponent<Animator>();
        shadowAnimator.SetBool("rise", true);
    }
    public void startHeadIdle()
    {
        var ravenHead = GameObject.Find("raven-spider_head");
        var ravenBeak = GameObject.Find("beakWrapper");
        ravenHead.GetComponent<Animator>().SetBool("startIdle", true);
        ravenBeak.GetComponent<Animator>().SetBool("startIdle", true);
        StartCoroutine("TriggerStabAttack");
    }

    IEnumerator TriggerStabAttack()
    {
        Debug.Log("stab remote trigger");
        yield return new WaitForSeconds(1.0f);
        var stabController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
        stabController.SetBool("stab", true);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
