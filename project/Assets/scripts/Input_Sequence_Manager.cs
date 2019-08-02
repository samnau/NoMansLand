using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Sequence_Manager : MonoBehaviour {
    public string[] keySequence = { "1", "2", "3" };
    public string[][] keyCombos;

    int currentKeyIndex = 0;

    float comboDelay = 0.5f;

	// Use this for initialization
	void Start () {
        keyCombos[0] = new string[] { "a", "4" };
	}
	void validateKeyPress()
    {
        var keyIsValid = correctKeyPressed();
        if(keyIsValid)
        {
            Debug.Log("correct key pressed!");
            currentKeyIndex++;
        }
    }

    bool correctKeyPressed()
    {
        var startTime = Time.time;
        var currentKeyPressed = Input.inputString;
        var validKeyPress = keySequence[currentKeyIndex];
        return currentKeyPressed == validKeyPress;
    }

	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown)
        {
            validateKeyPress();
        }
	}
}
