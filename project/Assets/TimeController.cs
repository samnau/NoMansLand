using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {
    public GameObject TargetParent;
    float slomoSpeed = 0.01f;
	// Use this for initialization
	void Start () {
        TargetParent = GameObject.FindGameObjectWithTag("Enemy");
	}
	public void FreezeTime()
    {
        var mainAnimator = TargetParent.GetComponent<Animator>();
        var animationArray = TargetParent.GetComponentsInChildren<Animator>();
        foreach(Animator targetAnimation in animationArray)
        {
            targetAnimation.speed = slomoSpeed;
        }
         mainAnimator.speed = slomoSpeed;
       // Time.timeScale = slomoSpeed;
        //Time.fixedDeltaTime = Time.timeScale * .01f;

    }
	// Update is called once per frame
	void Update () {
		
	}
}
