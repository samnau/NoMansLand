using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShieldAnimationSequencer : MonoBehaviour {
    float ShieldRevealDelay = 0f;
    Transform FireCore;
    AnimationStateTrigger FlameTrigger;
    Transform Flame;
    AnimationStateTrigger CoreTrigger;
    AnimationStateTrigger WrapperTrigger;
	// Use this for initialization
	void Start () {
        FireCore = gameObject.transform.Find("fire_shield_core");
        Flame = gameObject.transform.Find("fire_shield_flame");
        CoreTrigger = FireCore.GetComponent<AnimationStateTrigger>();
        FlameTrigger = Flame.GetComponent<AnimationStateTrigger>();
        WrapperTrigger = gameObject.GetComponent<AnimationStateTrigger>();
    }
	
   public void ShowFireShield()
    {
        //gameObject.GetComponent<Animator>().SetBool("appear", true);
        StartCoroutine("TriggerFlameAppear");
    }

     IEnumerator TriggerFlameAppear()
    {
        //yield return new WaitForSeconds(ShieldRevealDelay);
        FlameTrigger.TriggerChange = true;
        yield return new WaitForSeconds(0.5f);
        CoreTrigger.TriggerChange = true;
        WrapperTrigger.TriggerChange = true;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
