using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStateTracker : MonoBehaviour {
	string currentKeyPressed;
	string lastKeyPressed;
	string lastKeyReleased;
	public string direction = "down";
	public bool isWalking = false;
	string[] directionValues = {"left", "right", "up", "down" };

	// lastKeyPressed should always determine directon?
	// Use this for initialization
//	void Start () {
//
//	}
//	delegate bool InputMethod(string name);

//	InputMethod inputMethod;

	void printUserInput (string inputValue){

		if (Input.anyKeyDown) {
			print("input string: " + inputValue);
		}

	}
//	void inputChecker(InputMethod inputMethod){
//		if (InputMethod ("left")) {
//			printUserInput("left");
//		}
//		if (InputMethod ("right")) {
//			printUserInput("right");
//		}
//		if (InputMethod ("up")) {
//			printUserInput("up");
//		}
//		if (InputMethod ("down")) {
//			printUserInput("down");
//		}
//	}
	private void setCurrentKeyPressed(){
		foreach(string value in directionValues){
			if(Input.GetKey (value)){
				isWalking = true;
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
				isWalking = false;
				printUserInput("current key released " + value);
				lastKeyReleased = value;
			}
		}
	}

	void inputStateTracker(){
		if(Input.anyKey){
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
	void Start(){
//		inputMethod = Input.GetKey;
	}
}
