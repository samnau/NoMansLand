using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Dialog_Manager : MonoBehaviour
{
    [SerializeField] Image speakerPortrait;
    [SerializeField] Text text_dialog, text_speakerName;
    GameObject dialogWrapper;
    GameObject player;
    Animator dialogWrapperAnimator;
   // public YarnProgram scriptToLoad;
    [SerializeField]
    protected YarnProgram targetDialog;
    [SerializeField]
    public string targetText;
    protected DialogueRunner dialogueRunner;
    protected DialogueUI dialogueUI;
    InputStateTracker inputTracker;
    HeroMotionController motionController;
    bool dialogActive = false;
    // demo items
    AudioSource doorOpen;
    GameObject leftDoor;

    void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.Add(targetDialog);
        dialogWrapper = GameObject.Find("DialogElements");
        dialogWrapperAnimator = dialogWrapper.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        inputTracker = player.GetComponent<InputStateTracker>();
        motionController = player.GetComponent<HeroMotionController>();
        leftDoor = GameObject.Find("door left");
        doorOpen = leftDoor.GetComponent<AudioSource>();
        print(targetDialog);
    }
    void AdvanceDialog()
    {
        dialogueUI.MarkLineComplete();
    }
    void TogglePlayerMotion()
    {
        inputTracker.enabled = !dialogActive;
        motionController.enabled = !dialogActive;
    }
    public void BeginDialog()
    {
        dialogActive = true;
        dialogueRunner.startNode = targetText;
        dialogueRunner.StartDialogue(targetText);
        dialogWrapperAnimator.SetBool("show", dialogActive);
        TogglePlayerMotion();
    }

    public void NextDialogLine()
    {
        dialogueUI.MarkLineComplete();
    }
    IEnumerator sceneTransition()
    {
        var sceneCover = GameObject.Find("SceneCover");
        var coverAnimator = sceneCover.GetComponent<Animator>();
        coverAnimator.Play("show");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("MusicPlayer").SetActive(false);
        SceneManager.LoadScene("TitleCard");
    }
    public void EndDialog()
    {
        dialogActive = false;
        dialogWrapperAnimator.SetBool("show", dialogActive);
        dialogueRunner.ResetDialogue();
        //demo code only
        if(targetText == "LeftEntranceDoor")
        {
            StartCoroutine("sceneTransition");
            
            //SceneManager.LoadScene("TitleCard");
        }
        TogglePlayerMotion();
    }

}
