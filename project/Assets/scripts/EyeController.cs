using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public GameObject leftEyeBrow;
    public GameObject rightEyeBrow;
    Animator leftEyeBrowAnimator;
    Animator rightEyeBrowAnimator;
    // Start is called before the first frame update
    void Start()
    {
        leftEyeBrowAnimator = leftEyeBrow.GetComponent<Animator>();
        rightEyeBrowAnimator = rightEyeBrow.GetComponent<Animator>();
        StartCoroutine("SurpriseDemo");
    }

    IEnumerator SurpriseDemo()
    {
        yield return new WaitForSeconds(6.0f);
        TriggerSurprise();
    }

    IEnumerator SurpriseDemo2()
    {
        yield return new WaitForSeconds(6.0f);
        TriggerIdle();
    }

    void TriggerSurprise()
    {
        leftEyeBrowAnimator.SetBool("surprise", true);
        rightEyeBrowAnimator.SetBool("surprise", true);
        StartCoroutine("SurpriseDemo2");
    }

    void TriggerIdle()
    {
        leftEyeBrowAnimator.SetBool("surprise", false);
        rightEyeBrowAnimator.SetBool("surprise", false);
        StartCoroutine("SurpriseDemo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
