using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Key_Validator : MonoBehaviour
{
    public string[] keyCombo = new string[] { "", "" };
    public bool comboPressed = false;

    private void Start()
    {
        keyCombo = new string[] { "", "" };
    }

    IEnumerator resetValidCombo()
    {
        yield return new WaitForSeconds(0.1f);
        comboPressed = false;
    }

    void validateAnyKeyInCombo()
    {
        if (keyCombo[0]== "")
        {
            Debug.Log("no combo set");
            return;
        }
        comboPressed = Input.GetKey(keyCombo[0]) && Input.GetKey(keyCombo[1]);
        if(comboPressed)
        {
            resetValidCombo();
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
