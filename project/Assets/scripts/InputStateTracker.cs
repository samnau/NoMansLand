using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputStateTracker : MonoBehaviour {
	string currentKeyPressed;
	string lastKeyPressed;
	string lastKeyReleased;
	public string direction = "down";
	public bool isWalking = false;
	public bool isRunning = false;
	string[] directionValues = {"left", "right", "up", "down" };

	void printUserInput (string inputValue){

		if (Input.anyKeyDown) {
			print("input string: " + inputValue);
		}

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
	private void setCurrentKeyPressed(){
		foreach(string value in directionValues){
			if(Input.GetKey (value)){
				printUserInput("current key held " + value);
				currentKeyPressed = value;
				direction = value;
			}
		}
	}

	private void setCurrentKeyDown(){
		foreach(string value in directionValues){
			if(Input.GetKeyDown (value)){
				printUserInput("current key down " + value);
				lastKeyPressed = value;
			}
		}
	}

	void setCurrentReleased(){
		foreach(string value in directionValues){
			if(Input.GetKeyUp (value)){
				printUserInput("current key released " + value);
				lastKeyReleased = value;
			}
		}
	}

	void inputStateTracker(){
		isWalking = directionKeyPressed();
		isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;
		if (Input.anyKey){
			setCurrentKeyPressed();
		}else{
			setCurrentReleased();
		}
		if(Input.anyKeyDown){
			setCurrentKeyDown();
		}
	}
	// Update is called once per frame
	void Update () {
		inputStateTracker();
	}
}
