using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Key_Validator : MonoBehaviour
{
    public string[] keyCombo;
    public bool comboPressed = false;

    void validateAnyKeyInCombo()
    {
       // Debug.Log("key pressed was" + Input.inputString);
       if (Input.GetKey(keyCombo[0]) && Input.GetKey(keyCombo[1]))
        {
            Debug.Log("Combo!");
        }
    }


    void Update()
    {
        if (Input.anyKeyDown)
        {
            validateAnyKeyInCombo();
        }
    }
}
