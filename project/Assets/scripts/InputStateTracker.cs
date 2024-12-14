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
	public bool isUiActive = false;
	public bool isBattleActive = false;
	[HideInInspector]
	public string[] directionValues = {"left", "right", "up", "down" };
	HeroShadowController heroShadowController;

	public enum StartDirections
	{ up, down, left, right }

	public StartDirections startDirection;

    private void Start()
    {
		heroShadowController = FindObjectOfType<HeroShadowController>();
    }

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
					heroShadowController.TransformShadow();
				}
			}
		}
	}

	private void setCurrentKeyDown(){

		foreach(string value in directionValues){
			if(Input.GetKeyDown (value) && directionCanChange){
				directionCanChange = false;
				lastKeyPressed = value;
				heroShadowController.TransformShadow();
			}
		}
	}

	void setCurrentReleased(){

		foreach (string value in directionValues){
			if(Input.GetKeyUp (value)){
				lastKeyReleased = value;
				heroShadowController.TransformShadow();
			}
		}
	}

	void CheckLastKeyReleased()
    {
		if(lastKeyPressed == lastKeyReleased && lastKeyPressed != null)
        {
			lastKeyPressed = lastKeyReleased = null;
			directionCanChange = true;
			heroShadowController.TransformShadow();
		}
	}

	public void DisableMovement()
    {
		isUiActive = true;
    }

	public void EnableMovement()
    {
		isUiActive = false;
    }

	void inputStateTracker(){
		CheckLastKeyReleased();
		isWalking = directionKeyPressed();
		//COMEBACK: disable running for demo
		//isRunning = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isWalking;
		if(isUiActive || isBattleActive)
        {
			return;
        }
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
