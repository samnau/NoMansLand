using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSpiderController : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;
    [SerializeField] GameEvent introStart;
    [SerializeField] GameEvent familiarSummonStart;
    PositionTweener positionTweener;

    bool introStarted = false;
    bool introComplete = false;

    public void TriggerRise()
    {
        if (!introComplete)
        {
            gameObject.GetComponent<Animator>().SetBool("RISE", true);
            introComplete = true;
        }
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
