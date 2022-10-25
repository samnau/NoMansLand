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
    void Start()
    {
        dialogManager = FindObjectOfType<Dialog_Manager>();
        player = GameObject.FindGameObjectWithTag("Player");
        interactionIndicator = player.transform.Find("InteractionIndicator").gameObject;
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
