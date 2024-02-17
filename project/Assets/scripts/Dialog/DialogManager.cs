using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Yarn.Unity;

public class DialogManager : MonoBehaviour
{
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
    [SerializeField] Text SpeakerText;
    string defaultName = "Molly";


    [SerializeField] List<GameObject> dialogSpeakers;

    GameObject currentSpeaker;
    GameObject nextSpeaker;
    //[SerializeField] List<GameObject> dialogMarks;

    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler(
         "PlayInteractionSound",
          PlayInteractionSound
        );

        dialogueRunner.AddCommandHandler(
         "SetSpeakerName",
          SetSpeakerName
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
        if(dialogSpeakers.Count > 1)
        {
            nextSpeaker = dialogSpeakers[1];
        }

        if(dialogSpeakers.Count >= 1)
        {
            currentSpeaker = dialogSpeakers[0];
        }
    }

    public void SetSpeakerName(string[] parameters)
    {
        string name = parameters[0];
        Debug.Log(name);
        if (name == null)
        {
            SpeakerText.text = defaultName;
            return;
        }
        if (name.Contains("-"))
        {
            name = name.Replace("-", " ");
        }
        SpeakerText.text = name;
        SwapSpeakerPortraits();
    }

    IEnumerator HideSpeaker(GameObject targetSpeaker)
    {
        targetSpeaker?.GetComponent<Animator>()?.SetBool("DIALOG_SHOW", false);
        yield return new WaitForEndOfFrame();
        nextSpeaker = targetSpeaker;
    }

    void ShowSpeaker(GameObject targetSpeaker)
    {
        targetSpeaker?.GetComponent<Animator>()?.SetBool("DIALOG_SHOW", true);
        currentSpeaker = targetSpeaker;
    }

    void SwapSpeakerPortraits(int currentSpeakerIndex = 0, int newSpeakerIndex = 1)
    {
        if (dialogSpeakers.Count == 0)
        {
            return;
        }

        StartCoroutine(HideSpeaker(currentSpeaker));
        ShowSpeaker(nextSpeaker);
    }

    public void PlayInteractionSound(string[] parameter)
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
        print($"targetText= {targetText}");
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
        if (targetText == "LeftEntranceDoor")
        {
            StartCoroutine("sceneTransition");
        }
        TogglePlayerMotion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogActive)
        {
            NextDialogLine();
        }
    }

}
