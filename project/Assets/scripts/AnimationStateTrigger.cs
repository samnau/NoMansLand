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
        CurrentAnimator.SetBool(TargetStateName, true);
        TriggerChange = false;
        StartCoroutine("ResetState");
    }

    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(1.5f);
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
