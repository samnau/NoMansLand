using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class InteractionTrigger : MonoBehaviour
{
//    protected Dialog_Manager dialogManager;
    protected DialogManager dialogManager;

    [SerializeField]
    protected string targetText;
    bool dialogActive = false;
    bool triggerActive = false;
    GameObject player;
    GameObject interactionIndicator;
    AudioSource interactionSound;
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        interactionSound = gameObject.GetComponent<AudioSource>();

        interactionIndicator = player.transform.Find("InteractionIndicator")?.gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && triggerActive && !dialogActive)
        {
            dialogActive = true;
            dialogManager.BeginDialog();
            // demo code only - REMOVE LATER
            if (targetText == "Wall")
            {
                dialogManager.CameraEvent.Invoke();
            }
        } else if (Input.GetKeyDown(KeyCode.Space) && triggerActive && dialogActive)
        {
            dialogManager.NextDialogLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return;
        }

        if (interactionSound != null)
        {
            dialogManager?.SetInteractionSound(interactionSound);
        }

        interactionIndicator?.SetActive(true);
        if(dialogManager)
        {
            dialogManager.targetText = targetText;
        } else
        {

        }
        triggerActive = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactionIndicator.SetActive(false);
        triggerActive = false;
        dialogActive = false;
    }
}
