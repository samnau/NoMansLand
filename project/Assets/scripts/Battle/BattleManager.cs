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
        yield return new WaitForSeconds(1.5f);
        // NOTE: current home for the battle scene intro kickoff
        heroAnimator.SetBool("BATTLE_START", true);
        yield return new WaitForSeconds(.25f);
        heroAnimator.SetBool("BATTLE_START", false);
    }

}
