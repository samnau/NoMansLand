using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Sequence_Manager : MonoBehaviour {
    public string[] keySequence = { "1", "2", "3" };
    int currentKeyIndex = 0;

	// Use this for initialization
	void Start () {
		
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
