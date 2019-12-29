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
        startDelay = Random.Range(0.25f, 0.5f);
      // StartCoroutine("TriggerIdle");
    }

    IEnumerator TriggerIdle() {
        Debug.Log("Idle start");
        if(pauseIdle)
        {
            legAnimator.SetBool("startIdle", false);
            Debug.Log("no idle now");
            yield return null;
        }
       yield return new WaitForSeconds(startDelay);
        legAnimator.SetBool("startIdle", true);
        yield return new WaitForSeconds(2.5f);
        legAnimator.SetBool("startIdle", false);
        StartCoroutine("TriggerIdle");
    }
}
