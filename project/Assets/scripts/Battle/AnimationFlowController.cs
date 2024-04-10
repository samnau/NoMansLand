using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlowController : MonoBehaviour
{
    Animator animator;
    [HideInInspector] public bool rewindActive = false;
    [HideInInspector] public bool freezeActive = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void FreezeAnimation()
    {
        animator.speed = 0;
    }

    public void RewindAnimation()
    {
        animator.speed = 3f;
        animator.SetFloat("SPEED", -1f);
        StartCoroutine(CounterSpellSequence());
        // NOTES using a multiplier to reverse the animation speed works
        // Need to convert the time control code to always use the multiplier
    }

    public void UnFreezeAnimation()
    {
        animator.speed = 1;
    }

    public void TriggerCounterSequence()
    {
        freezeActive = true;
        StartCoroutine(CounterSpellSequence());
    }

    public void TriggerAnimationFreeze()
    {
        freezeActive = true;
        //FreezeAnimation();

        // just for testing
        RewindAnimation();
    }

    public void TriggerAnimationPause(float pauseDuration = .25f)
    {
        if(!freezeActive)
        {
            StartCoroutine(PauseAnimation(pauseDuration));
        }
    }

    public void TriggerSpeedChange(float targetSpeed = 1f)
    {
        if (!freezeActive)
        {
            StartCoroutine(AlterAnimationSpeed(targetSpeed));
        }
    }

    IEnumerator PauseAnimation(float pauseDuration = .25f)
    {
        if (freezeActive)
        {
            yield break;
        }
        //animator.speed = 0;
        FreezeAnimation();

        yield return new WaitForSeconds(pauseDuration);
        if (freezeActive)
        {
            yield break;
        }
        UnFreezeAnimation();
    }

    IEnumerator AlterAnimationSpeed(float targetSpeed = 1f, float duration = .75f)
    {
        if(freezeActive)
        {
            yield break;
        }
        print($"freeze is active: {freezeActive}");
        animator.speed = targetSpeed;
        yield return new WaitForSeconds(duration);
        if (freezeActive)
        {
            animator.speed = 1f;
            yield break;
        }
        animator.speed = 1f;

    }

    public void TriggerReset()
    {
        if(rewindActive)
        {
            animator.SetBool("RESET", true);
            animator.SetFloat("SPEED", 1f);
            this.GetComponent<BaseMonster>()?.TriggerBattleChallenge();
            rewindActive = false;
        }
    }

    IEnumerator CounterSpellSequence()
    {
        //RewindAnimation();
        animator.speed = 1f;
        animator.SetFloat("SPEED", .1f);
        yield return new WaitForSeconds(1f);
        animator.SetFloat("SPEED", 0f);
        yield return new WaitForSeconds(.5f);
        rewindActive = true;
        animator.SetFloat("SPEED", -.5f);
        yield return new WaitForSeconds(1.5f);
        animator.SetFloat("SPEED", -2f);
        yield return new WaitForSeconds(1.5f);
        //animator.SetBool("RESET", true);
        // NOTE: test code, temporary
        //this.GetComponent<BaseMonster>()?.TriggerBattleChallenge();
        //animator.SetFloat("SPEED", 1f);
    }

}
