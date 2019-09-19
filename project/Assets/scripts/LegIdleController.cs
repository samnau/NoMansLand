using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIdleController : MonoBehaviour {
    public bool pauseIdle;
    Animator legAnimator;
    float startDelay;

    // Use this for initialization
    void Start() {
        legAnimator = gameObject.GetComponent<Animator>();
        startDelay = Random.Range(0.5f, 1.0f);
        StartCoroutine("TriggerIdle");
    }

    IEnumerator TriggerIdle() {
        Debug.Log("Idle start");
       yield return new WaitForSeconds(startDelay);
        legAnimator.SetBool("startIdle", true);
        yield return new WaitForSeconds(2.0f);
        legAnimator.SetBool("startIdle", false);
        StartCoroutine("TriggerIdle");
    }
}
