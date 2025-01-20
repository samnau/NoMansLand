using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    protected Button button;
    [SerializeField]
    protected bool isDisabled = false;
    [SerializeField]
    protected bool isHidden = false;
    [SerializeField]
    protected bool selected = false;
    [SerializeField]
    protected bool requireConfirmation = false;

    protected GameStateManager gameStateManager;
    protected DataPersistanceManager dataPersistanceManager;

    protected bool initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        ButtonInit();
    }

    private void OnEnable()
    {
        if (initialized)
        {
            return;
        }
        ButtonInit();
        button.onClick.RemoveAllListeners();

    }

    private void OnDisable()
    {
        initialized = false;
        button.onClick.RemoveAllListeners();
    }

    protected virtual void ButtonInit()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        dataPersistanceManager = FindObjectOfType<DataPersistanceManager>();

        button = GetComponent<Button>();
        button.interactable = !isDisabled;
        gameObject.SetActive(!isHidden);
        if (selected)
        {
            button.Select();
        }
        initialized = true;
    }

}
