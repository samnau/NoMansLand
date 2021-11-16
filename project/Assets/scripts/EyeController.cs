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
    void Awake()
    {
        Debug.Log("eye start?");
        leftEyeBrowAnimator = leftEyeBrow.GetComponent<Animator>();
        rightEyeBrowAnimator = rightEyeBrow.GetComponent<Animator>();
    //    StartCoroutine("SurpriseDemo");
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

    public void SwitchExpression(string expressionValue)
    {
        switch(expressionValue)
        {
            case "surprise":
                TriggerSurprise();
                break;
            default:
                TriggerIdle();
                break;
        }
    }
    void TriggerAnimator(string targetExpression, bool expressionValue = false)
    {
       // mouthAnimator.SetBool(targetExpression, expressionValue);
    }
    void TriggerSurprise()
    {
        Debug.Log(leftEyeBrowAnimator);
        leftEyeBrowAnimator.SetBool("surprise", true);
        rightEyeBrowAnimator.SetBool("surprise", true);
       // StartCoroutine("SurpriseDemo2");
    }

    void TriggerIdle()
    {
        leftEyeBrowAnimator.SetBool("surprise", false);
        rightEyeBrowAnimator.SetBool("surprise", false);
        //StartCoroutine("SurpriseDemo");
    }

}
