using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Yarn.Unity;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_dialog, text_speakerName;
    GameObject dialogWrapper;
    GameObject player;
    Animator dialogWrapperAnimator;
    // new animator reference
    [SerializeField]
    Animator dialogUiAnimator;
    [SerializeField]
    protected YarnProject targetDialog;
    [SerializeField]
    public string targetText;
    protected DialogueRunner dialogueRunner;
    //protected DialogueUI dialogueUI;
    public DialogueAdvanceInput advanceInput;
    protected LineView dialogLineView;
    InputStateTracker inputTracker;
    HeroMotionController motionController;
    bool dialogActive = false;
    AudioSource interactionPlayer;
    public UnityEvent CameraEvent = new UnityEvent();
    [SerializeField] TextMeshProUGUI SpeakerText;
    string defaultName = "Molly";

    [SerializeField] bool isCutScene = false;
    [SerializeField] bool autoStart = false;
    [SerializeField] GameEvent SceneEnd;
    [SerializeField] GameEvent TutorialEnd;

    [SerializeField] List<GameObject> dialogSpeakers;

    GameObject currentSpeaker;
    GameObject nextSpeaker;

    [SerializeField] GameEvent DialogNodeComplete;

    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        advanceInput = FindAnyObjectByType<DialogueAdvanceInput>();
        dialogueRunner.AddCommandHandler(
         "PlayInteractionSound",
          PlayInteractionSound
        );

        dialogueRunner.AddCommandHandler<string>(
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
        //dialogueUI = GetComponent<DialogueUI>();
        dialogLineView = GetComponent<LineView>();
        dialogueRunner.SetProject(targetDialog);
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
        // THIS IS JUST DEGUGGING CODE BELOW
        if(autoStart)
        {
            BeginDialog();
        }
        //BeginDialog();
        //dialogWrapperAnimator.SetBool("show", true);
    }

    // TODO: refactor this now that the name is part of the text file
    public void SetSpeakerName(string name)
    {
        //string name = parameters[0];
        //if (name == null)
        //{
        //    SpeakerText.text = defaultName;
        //    return;
        //}
        //if (name.Contains("-"))
        //{
        //    name = name.Replace("-", " ");
        //}
        //SpeakerText.text = name;
        SwapSpeakerPortraits();
    }

    //public void SetSpeakerName(string[] parameters)
    //{
    //    string name = parameters[0];
    //    if (name == null)
    //    {
    //        SpeakerText.text = defaultName;
    //        return;
    //    }
    //    if (name.Contains("-"))
    //    {
    //        name = name.Replace("-", " ");
    //    }
    //    SpeakerText.text = name;
    //    SwapSpeakerPortraits();
    //}

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

    public void PlayInteractionSound()
    {
        interactionPlayer?.Play();
    }
    public void SetInteractionSound(AudioSource targetSoundPlayer)
    {
        interactionPlayer = targetSoundPlayer;
    }

    IEnumerator TriggerTogglePlayerMotion()
    {
        yield return new WaitForSeconds(0.2f);
        TogglePlayerMotion();
    }

    IEnumerator TriggerShowDialogAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        dialogUiAnimator.SetBool("show", true);

    }
    // NOTE: convert this to an event broadcast that the player can consume and disable input
    void TogglePlayerMotion()
    {
        inputTracker.enabled = !dialogActive;
        motionController.enabled = !dialogActive;

        // adding in code for when the input tracker has disabled itself
        // REFACTOR: needs simplicity and less function overlap
        if(dialogActive)
        {
            inputTracker.DisableMovement();
        } else
        {
            inputTracker.EnableMovement();
        }
    }
    public void BeginDialog()
    {
        dialogActive = true;
        //print($"I am at the begin dialog for Dialog Manager and this is dialogWrapper animator: {dialogWrapperAnimator}");
        //dialogUiAnimator.SetBool("show", true);
        StartCoroutine(TriggerShowDialogAnimation());

        dialogueRunner.startNode = targetText;
        dialogueRunner.StartDialogue(targetText);
        //dialogWrapperAnimator.SetBool("show", dialogActive);
        StartCoroutine(TriggerTogglePlayerMotion());

//        TogglePlayerMotion();
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
        //dialogueUI.MarkLineComplete();
        advanceInput.dialogueView.UserRequestedViewAdvancement();
    }

    public void StartCutScene()
    {
        advanceInput.enabled = false;
    }
    public void TriggerAdvanceCutSceneDialog ()
    {
        StartCoroutine(AdvanceCutSceneDialog());
    }
    IEnumerator AdvanceCutSceneDialog()
    {
        advanceInput.enabled = true;
        NextDialogLine();
        yield return new WaitForSeconds(.1f);
        advanceInput.enabled = false;
    }

    public void EndCutScene()
    {
        advanceInput.enabled = true;
    }
    void LineDismiss()
    {
        print("line dismissed");
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
        // Look at what the correct replacement is in this context
        //dialogueRunner.ResetDialogue();
        //demo code only - REMOVE LATER
        if (targetText == "LeftEntranceDoor")
        {
            GameObject.Find("MusicPlayer").SetActive(false);
            SceneManager.LoadScene("BattleDemoMenu");
            //StartCoroutine("sceneTransition");
        }
        TogglePlayerMotion();
    }

    void TriggerEndScene()
    {
        SceneEnd?.Invoke();
    }

    void TriggerEndTutorial()
    {
        TutorialEnd?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogActive && !isCutScene)
        {
            //NextDialogLine();
        }
    }

}
