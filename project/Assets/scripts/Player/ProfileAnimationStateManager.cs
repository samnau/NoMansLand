using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileAnimationStateManager : MonoBehaviour
{
    Animator animator;
    public string targetState = "IDLE";

    [SerializeField] GlowTweener mainGemGlow;
    [SerializeField] GlowTweener staffGemsGlow;

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

    public void TriggerBlueGlow()
    {
        TriggerStaffGlow(Color.blue, 10f, 1f, 9f);
    }

    public void TriggerRedGlow()
    {
        TriggerStaffGlow(Color.red, 10f, 1f, 9f);
    }

    public void TriggerYellowGlow()
    {
        TriggerStaffGlow(Color.yellow, 10f, 1f, 9f);
    }

    public void TriggerGreenGlow()
    {
        TriggerStaffGlow(Color.green, 10f, 1f, 9f);
    }

    public void GlowPulseUp()
    {
        mainGemGlow.TriggerGlowByDuration(12f, 1f);
        staffGemsGlow.TriggerGlowByDuration(12f, 1f);
    }

    public void GlowPulseDown()
    {
        mainGemGlow.TriggerGlowByDuration(4f, 1f);
        staffGemsGlow.TriggerGlowByDuration(4f, 1f);
    }

    void TriggerStaffGlow(Color targetColor, float targetIntensity, float duration = .5f, float colorIntensity = 7f)
    {
        if(!mainGemGlow || !staffGemsGlow)
        {
            return;
        }

        mainGemGlow.SetGlowColor(targetColor, colorIntensity);
        staffGemsGlow.SetGlowColor(targetColor, colorIntensity);

        mainGemGlow.TriggerGlowByDuration(targetIntensity, duration);
        staffGemsGlow.TriggerGlowByDuration(targetIntensity, duration);
    }

}
