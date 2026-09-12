using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarItemTrigger : MonoBehaviour
{
    InventoryConfirmationView inventoryConfirmationView;

    [Header("Familiar Configuration")]
    [SerializeField] 
    [HideInInspector]
    private string familiarId;
    public bool isCollectionTrigger = true;
    public bool disableOnComplete = false;
    [HideInInspector] public string completedDialog;
    GlobalInventoryState inventoryState;
    void Awake()
    {
        inventoryConfirmationView = FindObjectOfType<InventoryConfirmationView>(true);
        inventoryState = FindObjectOfType<GlobalInventoryState>();
    }

    private void Start()
    {
        OnCompleteHandler();
    }

    void SetTriggerType()
    {
        if(inventoryConfirmationView is null)
        {
            throw new System.InvalidOperationException("confirmation view not found");
        }
        inventoryConfirmationView.isCollectionConfirmation = isCollectionTrigger;
    }

    public void TriggerShowConfirmation()
    {
        SetTriggerType();
        inventoryConfirmationView?.ShowConfirmationView(familiarId);
    }

    public void TriggerHideConfirmmation()
    {
        inventoryConfirmationView?.HideConfirmationView();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetTriggerType();
        }
    }

    public bool IsFamiliarCollected()
    {
        if (inventoryState == null)
        {
            return false;
        }

        GlobalGameData.Familiar familiar = inventoryState.GetFamiliarById(familiarId);
        return familiar != null && familiar.collected;
    }

    public bool IsFamiliarActive()
    {
        if (inventoryState == null)
        {
            return false;
        }

        GlobalGameData.Familiar familiar = inventoryState.GetFamiliarById(familiarId);
        return familiar != null && familiar.active;
    }

    public void OnCompleteHandler()
    {
        if (disableOnComplete && isCollectionTrigger && IsFamiliarCollected())
        {
            gameObject.SetActive(false);
        }
    }

}
