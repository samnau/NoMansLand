using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesExpressionManager : MonoBehaviour
{
    Animator targetAnimator;
    EyeBlinkController eyeBlinkController;

    // Start is called before the first frame update
    void Start()
    {
        targetAnimator = GetComponent<Animator>();
        eyeBlinkController = GetComponent<EyeBlinkController>();
    }

    public void SetEyeExpression(string expressionValue)
    {
        ResetEyes();
        switch (expressionValue)
        {
            case "crying":
                TriggerSquint();
                break;
            default:
                TriggerIdle();
                break;
        }
    }

    void ResetEyes()
    {
        foreach (AnimatorControllerParameter parameter in targetAnimator.parameters)
        {
            targetAnimator.SetBool(parameter.name, false);
        }
    }

    void TriggerSquint()
    {
        targetAnimator.SetBool("squint", true);
    }

    void TriggerIdle()
    {
        ResetEyes();
        RestartBlink();
    }

    public void RestartBlink()
    {
        eyeBlinkController.startBlinkCycle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
