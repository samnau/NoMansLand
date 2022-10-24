using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionTrigger : MonoBehaviour
{
    protected Dialog_Manager dialogManager;
    [SerializeField]
    protected YarnProgram targetDialog;
    [SerializeField]
    protected string targetText;
    bool dialogActive = false;
    bool triggerActive = false;
    void Start()
    {
        dialogManager = FindObjectOfType<Dialog_Manager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && triggerActive && !dialogActive)
        {
            dialogActive = true;
            dialogManager.BeginDialog();
        } else if (Input.GetKeyDown(KeyCode.Space) && triggerActive && dialogActive)
        {
            dialogManager.NextDialogLine();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return;
        }
        dialogManager.targetText = targetText;
        triggerActive = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerActive = false;
        dialogActive = false;
    }
}
