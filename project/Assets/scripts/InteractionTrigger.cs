using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionTrigger : MonoBehaviour
{
    protected DialogueRunner dialogRunner;
    [SerializeField]
    protected YarnProgram targetDialog;
    protected DialogueUI dialogueUI;
    [SerializeField]
    protected string targetText;
    bool dialogActive = false;
    bool triggerActive = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogRunner = FindObjectOfType<DialogueRunner>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        dialogRunner.Add(targetDialog);
        dialogRunner.startNode = targetText;
    }

    void StartDialog()
    {
        dialogRunner.StartDialogue();
    }

    void AdvanceDialog()
    {
        dialogueUI.MarkLineComplete();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && triggerActive && !dialogActive)
        {
            dialogActive = true;
            StartDialog();
        } else if (Input.GetKeyDown(KeyCode.Space) && triggerActive && dialogActive)
        {
            AdvanceDialog();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.tag != "Player")
        {
            return;
        }
        //dialogRunner.StartDialogue();
        triggerActive = true;
        Debug.Log("triggered");
    }
}
