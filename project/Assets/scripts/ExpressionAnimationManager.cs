using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionAnimationManager : MonoBehaviour
{
    public EyeController eyeController;
    public MouthController mouthController;
    public Animator MouthAnimator;

    public void ChangeExpression(string eyeState="idle", string mouthState="idle")
    {
        eyeController.SwitchExpression(eyeState);
        mouthController.SwitchExpression(mouthState);
    }
}
