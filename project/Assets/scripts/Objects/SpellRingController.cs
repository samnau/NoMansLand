using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRingController : MonoBehaviour
{
    [SerializeField] GameObject hero;
    [SerializeField] GameObject familiar;
    Animator animator;
    [SerializeField] GameEvent familiarUnsummonComplete;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void Appear1()
    {
        animator.SetBool("APPEAR1", true);
    }

    public void ShowFamiliar()
    {
        //familiar.GetComponent<Animator>().SetBool("SUMMONED", true);
        familiar.GetComponent<AnimationStateTrigger>().TempTriggerAnimationState("SUMMONED");
    }

    public void HideFamiliar()
    {
        familiar?.GetComponent<BaseFamiliar>().HideFamiliar();
    }


    public void TriggerFamiliarUnSummonComplete()
    {
        familiarUnsummonComplete.Invoke();
    }

}
