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

    [SerializeField] GameEvent SceneEnd;
    [SerializeField] GameEvent TutorialEnd;

    [SerializeField] List<GameObject> dialogSpeakers;

    GameObject currentSpeaker;
    GameObject nextSpeaker;

    [SerializeField] GameEvent DialogNodeComplete;

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

        dialogueRunner.AddCommandHandler(
         "TriggerEndTutorial",
          TriggerEndTutorial
        );

        dialogueRunner.AddCommandHandler(
         "TriggerEndScene",
          TriggerEndScene
        );

    }
    void Start()
    {
        //dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueUI = GetComponent<DialogueUI>();
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
        print("set speaker name called");
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
    //void AdvanceDialog()
    //{
    //    dialogueUI.MarkLineComplete();
    //}

    // NOTE: convert this to an event broadcast that the player can consume and disable input
    void TogglePlayerMotion()
    {
        inputTracker.enabled = !dialogActive;
        motionController.enabled = !dialogActive;
    }
    public void BeginDialog()
    {
        print("being dialog");
        dialogActive = true;
        dialogueRunner.startNode = targetText;
        dialogueRunner.StartDialogue(targetText);
        dialogWrapperAnimator.SetBool("show", dialogActive);
        TogglePlayerMotion();
    }

    public void BeginTargetDialog(string dialogName)
    {
        print($"begin target dialog {dialogName}");
        dialogActive = true;
        dialogueRunner.startNode = dialogName;
        dialogueRunner.StartDialogue(dialogName);
        dialogWrapperAnimator.SetBool("show", dialogActive);
        TogglePlayerMotion();
    }

    public void TriggerDialogNodeComplete()
    {
        DialogNodeComplete.Invoke();
    }

    public void NextDialogLine()
    {
        dialogueUI.MarkLineComplete();
    }
    // REFACTOR: This is progress demo code that could be abstracted into something more useful
    IEnumerator sceneTransition()
    {
        //var sceneCover = GameObject.Find("SceneCover");
        //var coverAnimator = sceneCover.GetComponent<Animator>();
        //coverAnimator?.Play("show");
        //yield return new WaitForSeconds(1.5f);
        GameObject.Find("MusicPlayer").SetActive(false);
//        SceneManager.LoadScene("TitleCard");
        SceneManager.LoadScene("BattleDemoMenu");
        yield return null;
    }
    public void EndDialog()
    {
        dialogActive = false;
        dialogWrapperAnimator.SetBool("show", dialogActive);
        dialogueRunner.ResetDialogue();
        //demo code only - REMOVE LATER
        if (targetText == "LeftEntranceDoor")
        {
            GameObject.Find("MusicPlayer").SetActive(false);
            SceneManager.LoadScene("BattleDemoMenu");
            //StartCoroutine("sceneTransition");
        }
        TogglePlayerMotion();
    }

    void TriggerEndScene(string[] parameters)
    {
        SceneEnd?.Invoke();
    }

    void TriggerEndTutorial(string[] parameters)
    {
        TutorialEnd?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogActive)
        {
            NextDialogLine();
        }
    }

}
