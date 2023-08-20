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
    RuneIntroSequencer introSequencer;
    InputStateTracker inputStateTracker;
    [SerializeField] GameEvent familiarTakeDamage;
    [SerializeField] GameEvent startBattle;


    GameObject[] battleChallenges = new GameObject[3];
    bool showBattleUI = false;

    void Start()
    {
        heroAnimator = hero.GetComponent<Animator>();
        introSequencer = battleUI.GetComponentInChildren<RuneIntroSequencer>();
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        StartCoroutine("StartBattleSequence");
    }

    IEnumerator StartBattleSequence()
    {
        yield return new WaitForSeconds(.25f);
        inputStateTracker.isUiActive = true;
        print("A monster appears!");
        print("Molly starts to freak out.");
        yield return new WaitForSeconds(1.5f);
        print("Molly decides to fight");
        print("Summon the staff!");
        heroAnimator.Play("summon_staff");
        // **** TEMP: turning off the battle system for animation testing- lines 40-42
        //yield return new WaitForSeconds(2.5f);
        //startBattle.Invoke();
        //yield return new WaitForSeconds(.5f);

        // **** END testing disable block
        //var defenseCombos = monster.GetComponent<BaseMonster>().defenseCombos;
        //BattleCombo currentCombo = defenseCombos[0];
        //print($"Molly yells out: {currentCombo.keyCode1} + {currentCombo.keyCode2}");
        //monster.GetComponent<BaseMonster>().canDefend = true;
        //yield return new WaitForSeconds(2f);
        //if(!monster.GetComponent<BaseMonster>().defenseSuccess)
        //{
        //    familiarTakeDamage.Invoke();
        //}
            //introSequencer.TriggerIntroSequence();
    }

}
