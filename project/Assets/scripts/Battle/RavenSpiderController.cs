using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSpiderController : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;
    [SerializeField] GameEvent introStart;
    [SerializeField] GameEvent familiarSummonStart;
    [SerializeField] GameEvent startBattle;
    PositionTweener positionTweener;

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

    public void StartBattle()
    {
        if(battleStarted)
        {
            return;
        }
        battleStarted = true;
        startBattle.Invoke();
    }

    public void StartBattleIntro()
    {
        introStart?.Invoke();
    }

    public void StartFamiliarSummon()
    {
        familiarSummonStart?.Invoke();
    }
    public void StartIntro()
    {
        if(!introStarted)
        {
            dialogManager?.BeginDialog();
            introStarted = true;
        }
    }
}
