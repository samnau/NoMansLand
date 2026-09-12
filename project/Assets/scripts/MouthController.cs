using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    public Animator mouthAnimator;
    void Awake()
    {
       // mouthAnimator = GetComponent<Animator>();
    }
    // Possible values for expressions are:
    // smile, frown, smirk, surprise, annoyed, idle
    public void SwitchExpression(string expressionValue)
    {
        foreach (AnimatorControllerParameter parameter in mouthAnimator.parameters)
        {
            mouthAnimator.SetBool(parameter.name, false);
        }
        if (expressionValue != null)
        {
            TriggerAnimator(expressionValue, true);
        } else
        {
            TriggerAnimator("idle");
        }
    }

    void TriggerAnimator(string targetExpression, bool expressionValue=false)
    {
        mouthAnimator.SetBool(targetExpression, expressionValue);
    }

}
