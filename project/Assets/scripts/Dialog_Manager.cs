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
    [SerializeField]
    protected YarnProgram targetDialog;
    [SerializeField]
    public string targetText;
    protected DialogueRunner dialogueRunner;
    protected DialogueUI dialogueUI;
    InputStateTracker inputTracker;
    HeroMotionController motionController;
    bool dialogActive = false;
    AudioSource interactionPlayer;


    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler(
         "PlayInteractionSound",
          PlayInteractionSound
        );
    }
    void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueRunner.Add(targetDialog);
        dialogWrapper = GameObject.Find("DialogElements");
        dialogWrapperAnimator = dialogWrapper.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        inputTracker = player.GetComponent<InputStateTracker>();
        motionController = player.GetComponent<HeroMotionController>();
    }

    public void PlayInteractionSound (string[] parameter)
    {
        interactionPlayer?.Play();
    }
    public void SetInteractionSound(AudioSource targetSoundPlayer)
    {
        interactionPlayer = targetSoundPlayer;
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
        //demo code only - REMOVE LATER
        if(targetText == "LeftEntranceDoor")
        {
            StartCoroutine("sceneTransition");
        }
        TogglePlayerMotion();
    }

}
