using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileAnimationStateManager : MonoBehaviour
{
    Animator animator;
    public string targetState = "IDLE";
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void ToggleAnimationState(string targetState = "IDLE")
    {
        bool currentActiveState = animator.GetBool(targetState);
        animator.SetBool(targetState, !currentActiveState);
    }

    public void ActivateAnimationState(string targetState = "IDLE")
    {
        animator.SetBool(targetState, true);
    }

    public void DeactivateAnimationState(string targetState = "IDLE")
    {
        animator.SetBool(targetState, false);
    }

}
