using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarUseReceiver : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]
    [HideInInspector]
    string requiredFamiliar;
    [SerializeField]
    GameEvent completionEvent;
    
    private GlobalInventoryState inventoryState;

    void Start()
    {
        inventoryState = FindObjectOfType<GlobalInventoryState>();

        if(inventoryState is null)
        {
            Debug.LogWarning("No inventory state found");
        }
    }

    // Listen for a game event meant for general familiar use triggers
    public void CheckForRequiredFamiliar()
    {
        if (inventoryState == null || string.IsNullOrEmpty(requiredFamiliar))
        {
            return;
        }

        GlobalGameData.Familiar familiar = inventoryState.GetFamiliarById(requiredFamiliar);
        bool familiarUsed = familiar != null && familiar.used;
        
        print("CheckForRequiredFamiliar is fired");
        print(familiarUsed);
        if (familiarUsed && completionEvent != null)
        {
            completionEvent.Invoke();
            print("required familiar used");
        }
    }

}
