using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSpiderController : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;

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
    public void StartIntro()
    {
        if(!introStarted)
        {
            dialogManager?.BeginDialog();
            introStarted = true;
        }
    }
}
