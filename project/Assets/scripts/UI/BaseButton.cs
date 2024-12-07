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
    // Start is called before the first frame update
    void Start()
    {
        ButtonInit();
    }

    protected virtual void ButtonInit()
    {
        button = GetComponent<Button>();
        button.interactable = !isDisabled;
        gameObject.SetActive(!isHidden);
        if (selected)
        {
            button.Select();
        }
    }

}
