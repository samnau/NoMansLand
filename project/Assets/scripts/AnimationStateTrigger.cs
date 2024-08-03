using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateTrigger : MonoBehaviour {
    // GameObject CurrentObject;
    Animator CurrentAnimator;
    public string TargetState;
    public bool TriggerChange = false;

	// Use this for initialization
	void Start () {
        CurrentAnimator = gameObject.GetComponent<Animator>();
	}
	
    public void TriggerState(string TargetStateName)
    {
        if(TargetStateName == null)
        {
            return;
        }

        if(TargetStateName == "RECALL")
        {
            print("the state is recall");
        }
        CurrentAnimator.SetBool(TargetStateName, true);
        TriggerChange = false;
        StartCoroutine("ResetState");
    }

    public void TempTriggerAnimationState(string targetState = "IDLE")
    {
        StartCoroutine(TempAnimationState(targetState));
    }

    IEnumerator TempAnimationState(string targetState = "IDLE")
    {
        ActivateAnimationState(targetState);
        yield return new WaitForSeconds(.1f);
        DeactivateAnimationState(targetState);
    }

    public void ActivateAnimationState(string targetState = "IDLE")
    {
        CurrentAnimator.SetBool(targetState, true);
    }

    public void DeactivateAnimationState(string targetState = "IDLE")
    {
        CurrentAnimator.SetBool(targetState, false);
    }

    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(1.5f);
        if(TargetState == "")
        {
            yield break;
        }
        CurrentAnimator.SetBool(TargetState, false);
    }

    // Update is called once per frame
    void Update () {
		if(TriggerChange)
        {
            TriggerState(TargetState);
        }
	}
}
