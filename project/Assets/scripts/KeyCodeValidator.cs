using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeyCodeValidator : MonoBehaviour
{
    public KeyCode[] keyCombo = new KeyCode[] { KeyCode.Underscore, KeyCode.Underscore };
    public bool comboPressed = false;

    private void Start()
    {
        keyCombo = new KeyCode[] { KeyCode.W, KeyCode.UpArrow };
    }

    IEnumerator resetValidCombo()
    {
        yield return new WaitForSeconds(0.1f);
        comboPressed = false;
    }

    void validateAnyKeyInCombo()
    {
        if (keyCombo[0]== KeyCode.Underscore)
        {
            Debug.Log("no combo set");
            return;
        }
        comboPressed = Input.GetKey(keyCombo[0]) && Input.GetKey(keyCombo[1]);
        if(comboPressed)
        {
            Debug.Log("key combo codes pressed!");
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
