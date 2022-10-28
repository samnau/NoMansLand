using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionTrigger : MonoBehaviour
{
    protected Dialog_Manager dialogManager;
    [SerializeField]
    protected YarnProgram targetDialog;
    [SerializeField]
    protected string targetText;
    bool dialogActive = false;
    bool triggerActive = false;
    GameObject player;
    GameObject interactionIndicator;
    AudioSource doorOpen;
    AudioClip audioClip;
    GameObject leftDoor;
    AudioSource interactionSound;
    void Start()
    {
        dialogManager = FindObjectOfType<Dialog_Manager>();
        player = GameObject.FindGameObjectWithTag("Player");
        interactionSound = gameObject.GetComponent<AudioSource>();
        audioClip = interactionSound.clip;
        print(audioClip);
        interactionIndicator = player.transform.Find("InteractionIndicator").gameObject;
    }
    public void Awake()
    {
        var dialogRunner = FindObjectOfType<DialogueRunner>();
        // Create a new command called 'camera_look', which looks at a target.
        dialogRunner.AddCommandHandler(
            "PlayInteractionSound",     // the name of the command
            PlayInteractionSound // the method to run
        );
    }
    // InteractionIndicator
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && triggerActive && !dialogActive)
        {
            dialogActive = true;
            dialogManager.BeginDialog();
            // demo code only
            if(targetText == "TreeGate")
            {
                player.GetComponent<Animator>().Play("hero-up");
            }
        } else if (Input.GetKeyDown(KeyCode.Space) && triggerActive && dialogActive)
        {
            dialogManager.NextDialogLine();
        }
    }
    //[YarnCommand("PlayInteractionSound")]
    void PlayInteractionSound(string[] parameter)
    {
        if(audioClip != null)
        {
            print(audioClip);
            interactionSound.clip = audioClip;
            interactionSound.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return;
        }
        interactionIndicator.SetActive(true);
        dialogManager.targetText = targetText;
        triggerActive = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactionIndicator.SetActive(false);
        triggerActive = false;
        dialogActive = false;
    }
}
