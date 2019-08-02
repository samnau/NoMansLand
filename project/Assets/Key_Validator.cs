using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Key_Validator : MonoBehaviour
{
    public string[] keyCombo;
    public string[] keyComboCopy;
    string currentKeyToMatch;
    float comboDelay = 0.5f;
    bool withinWindow = false;
    public bool comboPressed = false;

    void validateAnyKeyInCombo()
    {
        for (int i = 0; i < keyCombo.Length; i++)
        {
            if (Input.GetKeyDown(keyCombo[i]))
            {
                Debug.Log(keyCombo.Where((val => val != keyCombo[i])).ToArray()[0]);
               currentKeyToMatch = keyCombo.Where((val => val != keyCombo[i])).ToArray()[0];
            }
        }

        if (currentKeyToMatch != null)
        {
            StartCoroutine(manageComboWindow());
        }
    }

    void validateOtherComboHalf()
    {
        if (withinWindow)
        {
            comboPressed = Input.GetKeyDown(currentKeyToMatch);
            Debug.Log(comboPressed);
        }
        currentKeyToMatch = null;
    }

    IEnumerator manageComboWindow()
    {
        withinWindow = true;
        yield return new WaitForSeconds(comboDelay);
        withinWindow = false;
    }

    void validateKeyPress()
    {
        if (keyCombo.Length == 0)
        {
            return;
        }
        if (currentKeyToMatch == null)
        {
            validateAnyKeyInCombo();
        }
        else
        {
            validateOtherComboHalf();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            validateKeyPress();
        }
    }
}
