using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject hero;
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject familiar;
    [SerializeField] GameObject monster;
    Animator heroAnimator;
    Animation heroAnimation;
    RuneIntroSequencer introSequencer;
    // Start is called before the first frame update
    void Start()
    {
        heroAnimator = hero.GetComponent<Animator>();
        introSequencer = battleUI.GetComponentInChildren<RuneIntroSequencer>();

        //        heroAnimator.Play("summon_staff");
        //heroAnimator.SetBool("BATTLE_START", true);
        StartCoroutine("StartBattleSequence");
    }

    IEnumerator StartBattleSequence()
    {
        yield return new WaitForSeconds(2f);
        heroAnimator.Play("summon_staff");
        yield return new WaitForSeconds(2f);
        introSequencer.TriggerIntroSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
