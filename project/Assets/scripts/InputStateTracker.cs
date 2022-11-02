using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputStateTracker : MonoBehaviour {
	string currentKeyPressed;
	string lastKeyPressed;
	string lastKeyReleased;
	bool directionCanChange = true;
	public string direction = "down";
	public bool isWalking = false;
	public bool isRunning = false;
	string[] directionValues = {"left", "right", "up", "down" };

	void printUserInput (string inputValue){
		print($"input string: {inputValue}");
	}

	private void logAnyKey (string inputValue)
    {
		if (Input.anyKey)
		{
			print("input string: " + inputValue);
		}
	}
	bool directionKeyPressed()
    {
		return directionValues.Any(direction => Input.GetKey(direction));
	}
	bool directionKeyReleased()
	{
		return directionValues.Any(direction => Input.GetKeyUp(direction));
	}
	private void setCurrentKeyPressed(){
		foreach(string value in directionValues){
			if(Input.GetKey (value)){
				currentKeyPressed = value;
				if(directionCanChange)
                {
					direction = value;
				}
			}
		}
	}

	private void setCurrentKeyDown(){

		foreach(string value in directionValues){
			if(Input.GetKeyDown (value) && directionCanChange){
				directionCanChange = false;
				lastKeyPressed = value;
			}
		}
	}

	void setCurrentReleased(){

		foreach (string value in directionValues){
			if(Input.GetKeyUp (value)){
			lastKeyReleased = value;
			}
		}
	}

	void CheckLastKeyReleased()
    {
		if(lastKeyPressed == lastKeyReleased && lastKeyPressed != null)
        {
			lastKeyPressed = lastKeyReleased = null;
			directionCanChange = true;
		}
    }

	void inputStateTracker(){
		CheckLastKeyReleased();
		isWalking = directionKeyPressed();
		isRunning = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isWalking;
		if (Input.anyKey){
			setCurrentKeyPressed();
		}
		if(Input.anyKeyDown){
			setCurrentKeyDown();
		}

		setCurrentReleased();
	}
	// Update is called once per frame
	void Update () {
		inputStateTracker();
	}
}
