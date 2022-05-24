using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Dialog_Manager : MonoBehaviour
{
    [SerializeField] Image speakerPortrait;
    [SerializeField] Text text_dialog, text_speakerName;
    // Start is called before the first frame update
    public YarnProgram scriptToLoad;

    public DialogueRunner dialogueRunner;
    public DialogueUI dialogueUI;

    void Start()
    {
        dialogueRunner.Add(scriptToLoad);
        print("start text");
    }

    public void BeginDialog()
    {
        dialogueRunner.StartDialogue();
    }

    public void NextDialogLine()
    {
        dialogueUI.MarkLineComplete();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
