using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSpiderController : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;

    bool introStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartIntro()
    {
        if(!introStarted)
        {
            dialogManager?.BeginDialog();
            introStarted = true;
        }
    }
    // Update is called once per frame
}
