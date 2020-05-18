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
    void ChangeTime(float timeSpeed)
    {
        var mainAnimator = TargetParent.GetComponent<Animator>();
        var animationArray = TargetParent.GetComponentsInChildren<Animator>();
        foreach (Animator targetAnimation in animationArray)
        {
            targetAnimation.speed = timeSpeed;
        }
        mainAnimator.speed = timeSpeed;
    }
    public void FreezeTime()
    {
        ChangeTime(slomoSpeed);
    }
    public void UnFreezeTime()
    {
        ChangeTime(1.0f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
