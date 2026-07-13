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
    [Header("Configuration")]
    [SerializeField] TextMeshProUGUI text_dialog, text_speakerName;
    GameObject dialogWrapper;
    GameObject player;
   // Animator dialogWrapperAnimator;
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
//    [SerializeField] TextMeshProUGUI SpeakerText;
  //  string defaultName = "Molly";

    [SerializeField] bool isCutScene = false;
    [SerializeField] bool autoStart = false;
    [Header("Game Events")]
    [SerializeField] GameEvent SceneEnd;
    [SerializeField] GameEvent TutorialEnd;
    [SerializeField] GameEvent FreezePlayer;
    [SerializeField] GameEvent UnfreezePlayer;
    [SerializeField] GameEvent DialogNodeComplete;

    [Header("Speakers")]
    [SerializeField] List<GameObject> dialogSpeakers;

    GameObject currentSpeaker;
    GameObject nextSpeaker;

    [HideInInspector]
    public InventoryItemTrigger inventoryItemTrigger;

    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        advanceInput = FindAnyObjectByType<DialogueAdvanceInput>();
        dialogueRunner.AddCommandHandler(
         "PlayInteractionSound",
          PlayInteractionSound
        );

        dialogueRunner.AddCommandHandler(
         "SwapSpeakers",
          TriggerSpeakerSwap
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
        dialogLineView = GetComponent<LineView>();
        dialogueRunner.SetProject(targetDialog);
        dialogWrapper = GameObject.Find("DialogElements");
        player = GameObject.FindGameObjectWithTag("Player");
        
        inputTracker = player?.GetComponent<InputStateTracker>();
        motionController = player?.GetComponent<HeroMotionController>();
        if(dialogSpeakers.Count > 1)
        {
            nextSpeaker = dialogSpeakers[1];
        }

        if(dialogSpeakers.Count >= 1)
        {
            currentSpeaker = dialogSpeakers[0];
        }

        if(autoStart)
        {
            BeginDialog();
        }
        if(isCutScene)
        {
            advanceInput.enabled = false;
            FreezePlayer.Invoke();
        }

        if(!FreezePlayer || !UnfreezePlayer )
        {
            Debug.LogWarning("Player input events not assigned");
        }
    }

    public void TriggerSpeakerSwap()
    {
        // This method has parameter defaults, but the command handler doesn't allow me to omit them, so I am calling this proxy method wrapper
        SwapSpeakerPortraits();
    }

    public void HideDialogUI()
    {
        dialogUiAnimator.SetBool("show", false);
    }

    public void ShowDialogUI()
    {
        dialogUiAnimator.SetBool("show", true);
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
    public void DisablePlayerMotion()
    {
        inputTracker.enabled = false;
        motionController.enabled = false;
    }

    public void FlipPlayerDirection()
    {
        Transform playerTransform = player.transform;
        Vector3 playerScale = playerTransform.localScale;
        playerTransform.localScale = new Vector3(playerScale.x * -1, playerScale.y, playerScale.z);
    }
    // NOTE: convert this to an event broadcast that the player can consume and disable input
    void TogglePlayerMotion()
    {
        // adding in code for when the input tracker has disabled itself
        // REFACTOR: needs simplicity and less function overlap
        if(dialogActive)
        {
            FreezePlayer?.Invoke();
            //inputTracker.DisableMovement();
        } else
        {
            UnfreezePlayer?.Invoke();
            //inputTracker.EnableMovement();
        }
    }
    public void BeginDialog()
    {
        dialogActive = true;
        StartCoroutine(TriggerShowDialogAnimation());

        dialogueRunner.startNode = targetText;
        dialogueRunner.StartDialogue(targetText);
        StartCoroutine(TriggerTogglePlayerMotion());
    }

    public void BeginTargetDialog(string dialogName)
    {
        dialogActive = true;
        dialogueRunner.startNode = dialogName;
        dialogueRunner.StartDialogue(dialogName);
        dialogUiAnimator.SetBool("show", dialogActive);
        TogglePlayerMotion();
    }

    public void TriggerDialogNodeComplete()
    {
        DialogNodeComplete.Invoke();
    }

    public void NextDialogLine()
    {
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
        dialogUiAnimator.SetBool("show", dialogActive);
        //demo code only - REMOVE LATER
        // commented out but not tested for issues after removal
        //if (targetText == "LeftEntranceDoor")
        //{
        //    GameObject.Find("MusicPlayer").SetActive(false);
        //    SceneManager.LoadScene("BattleDemoMenu");
        //    //StartCoroutine("sceneTransition");
        //}
        if (currentSpeaker == dialogSpeakers[1])
        {
            SwapSpeakerPortraits();
        }
        if (inventoryItemTrigger != null)
        {
            print($"collected dialog: {inventoryItemTrigger.collectedDialog}");
            inventoryItemTrigger.TriggerShowConfirmation();
            inventoryItemTrigger = null;
        } else
        {
            TogglePlayerMotion();
        }
    }

    void TriggerEndScene()
    {
        SceneEnd?.Invoke();
    }

    void TriggerEndTutorial()
    {
        TutorialEnd?.Invoke();
    }

}
