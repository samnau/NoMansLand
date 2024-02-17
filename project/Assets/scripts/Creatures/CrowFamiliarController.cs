using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFamiliarController : MonoBehaviour
{
    [SerializeField] GameObject halo;
    DialogManager dialogManager;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
    }

    public void HideHalo()
    {
        Animator haloNaimator = halo?.GetComponent<Animator>();
        haloNaimator?.SetBool("HIDE", true);
        dialogManager.targetText = "Win";
        dialogManager.BeginDialog();
    }

}
