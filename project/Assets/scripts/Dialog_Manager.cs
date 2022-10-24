using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Dialog_Manager : MonoBehaviour
{
    [SerializeField] Image speakerPortrait;
    [SerializeField] Text text_dialog, text_speakerName;
    GameObject dialogWrapper;
    Animator dialogWrapperAnimator;
    // Start is called before the first frame update
    public YarnProgram scriptToLoad;
    [SerializeField]
    protected YarnProgram targetDialog;
    [SerializeField]
    public string targetText;
    protected DialogueRunner dialogueRunner;
    protected DialogueUI dialogueUI;

    void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.Add(targetDialog);
        dialogWrapper = GameObject.Find("DialogElements");
        dialogWrapperAnimator = dialogWrapper.GetComponent<Animator>();
       print(targetDialog);
    }
    void AdvanceDialog()
    {
        dialogueUI.MarkLineComplete();
    }
    public void BeginDialog()
    {
        dialogueRunner.startNode = targetText;
        dialogueRunner.StartDialogue(targetText);
        dialogWrapperAnimator.SetBool("show", true);
    }

    public void NextDialogLine()
    {
        dialogueUI.MarkLineComplete();
    }
    public void EndDialog()
    {
        dialogWrapperAnimator.SetBool("show", false);
        dialogueRunner.ResetDialogue();
    }

}
