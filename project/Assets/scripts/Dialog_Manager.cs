using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Yarn.Unity;

public class Dialog_Manager : MonoBehaviour
{
    // NOTE: this field may be unused 
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
    public UnityEvent CameraEvent = new UnityEvent();

    [SerializeField] List<GameObject> dialogSpeakers;
    [SerializeField] List<GameObject> dialogMarks;

    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        //dialogueRunner.AddCommandHandler(
        // "PlayInteractionSound",
        //  PlayInteractionSound
        //);
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

    void HideSpeaker (GameObject targetSpeaker, bool tween = true)
    {
        targetSpeaker?.GetComponent<Animator>()?.SetBool("DIALOG_SHOW", false);
    }

    void ShowSpeaker(GameObject targetSpeaker)
    {
        targetSpeaker?.GetComponent<Animator>()?.SetBool("DIALOG_SHOW", true);
    }

    void SwapSpeakerPortraits (int currentSpeakerIndex = 0, int newSpeakerIndex = 1)
    {
        if(dialogSpeakers.Count == 0)
        {
            return;
        }

        GameObject currentSpeaker = dialogSpeakers[currentSpeakerIndex];
        GameObject newSpeaker = dialogSpeakers[newSpeakerIndex];
        HideSpeaker(currentSpeaker);
        ShowSpeaker(newSpeaker);
    }

    IEnumerator TestSpeakerSwap()
    {
        yield return new WaitForSeconds(1f);
        SwapSpeakerPortraits(0, 1);
        yield return new WaitForSeconds(2f);
        SwapSpeakerPortraits(1, 0);
    }

    public void PlayInteractionSound (string[] parameter)
    {
        interactionPlayer?.Play();
    }
    public void SetInteractionSound(AudioSource targetSoundPlayer)
    {
        interactionPlayer = targetSoundPlayer;
    }

    // NOTE: currently unused
    void AdvanceDialog()
    {
        dialogueUI.MarkLineComplete();
    }

    // NOTE: convert this to an event broadcast that the player can consume and disable input
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
        StartCoroutine(TestSpeakerSwap());
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
