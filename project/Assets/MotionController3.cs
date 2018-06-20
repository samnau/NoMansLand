using UnityEngine;
using System.Collections;

public class MotionController3 : MonoBehaviour
{
	string lastKeyPressed;
	string lastKeyReleased;
	string currentKeyDown;
	float horizontalAxisValue;
	float verticalAxisValue;
	bool isMoving;
	// Use this for initialization
	void Start ()
	{

	}
	void DirectionCheck(){
		verticalAxisValue = Input.GetAxis ("Vertical");
		horizontalAxisValue = Input.GetAxis ("Horizontal");
		isMoving = horizontalAxisValue != 0 || verticalAxisValue != 0;
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
	
	}
}

