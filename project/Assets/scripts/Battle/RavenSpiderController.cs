using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSpiderController : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;
    [SerializeField] GameEvent introStart;
    [SerializeField] GameEvent familiarSummonStart;
    [SerializeField] GameEvent startBattle;
    [SerializeField] GameEvent showFamiliar;

    PositionTweener positionTweener;
    int loopCount = 0;
    bool hasRoared = false;


    bool introStarted = false;
    bool introComplete = false;
    bool battleStarted = false;

    public void TriggerRise()
    {
        if (!introComplete)
        {
            gameObject.GetComponent<Animator>().SetBool("RISE", true);
            introComplete = true;
        }
    }

    public void TriggerRoar()
    {
        if(!hasRoared)
        {
            loopCount++;
        }
        if(loopCount > 0 && !hasRoared)
        {
            gameObject.GetComponent<Animator>().SetBool("ROAR", true);
            hasRoared = true;
            StartBattle();
        }
    }

    public void DisableRoar()
    {
        gameObject.GetComponent<Animator>().SetBool("ROAR", false);
    }

    public void StartBattle()
    {
        if(battleStarted)
        {
            return;
        }
        battleStarted = true;
        startBattle?.Invoke();
    }

    public void StartBattleIntro()
    {
        introStart?.Invoke();
    }

    public void StartFamiliarSummon()
    {
        familiarSummonStart?.Invoke();
    }

    public void StartFamiliarShow()
    {
        print("familiar show started");

        showFamiliar?.Invoke();
    }

    //NOTE: this triggers the battle intro
    public void StartIntro()
    {
        if(!introStarted)
        {
            dialogManager?.BeginDialog();
            introStarted = true;
        }
    }
}
