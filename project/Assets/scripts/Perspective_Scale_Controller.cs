using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective_Scale_Controller : MonoBehaviour {
	//float screenHeight = Screen.height;
	float Ypos;
	float prevYpos;
	float currentScale;
	float newScaleFactor;
	float newScale;
	bool positionIsChanged;
	bool isNotColliding = true;
	int scaleDirection;
	public float scaleFactor = 1.0f;
	float scaleAmount = 0.0020f;
	Vector3 screenPos;
	Vector3 scaleVector;
	Camera cameraObject;
//	float viewHeight;

	float getScreenPos(){
		screenPos = cameraObject.WorldToScreenPoint(transform.position);
		return screenPos.y;
	}
	private bool isMovingUp(){
		return Input.GetKey("up");
	}
	private bool isMovingDown(){
		return Input.GetKey("down");
	}
	private void setScaleDirection(){
		if (isMovingUp ()) {
			scaleDirection = -1;
		} else if (isMovingDown()) {
			scaleDirection = 1;
		}
	}
	// Use this for initialization
	void setPlayerScale(){
//		viewHeight = cameraObject.pixelHeight;
		Ypos = getScreenPos();
		positionIsChanged = Ypos != prevYpos;

		if (positionIsChanged && isNotColliding) {
			newScaleFactor = scaleDirection * scaleAmount * scaleFactor;
			currentScale = transform.localScale.x;
			newScale = currentScale + newScaleFactor;

			scaleVector = new Vector3 (newScale, newScale, 1);
			transform.localScale = scaleVector;
			prevYpos = Ypos;
		}

	}

	void OnCollisionEnter2D( Collision2D collision ){
		isNotColliding = false;
	}
	void OnCollisionStay2D( Collision2D collision ){
		isNotColliding = false;
	}
	void OnCollisionExit2D( Collision2D collision ){
		isNotColliding = true;
	}
	void Start () {
		currentScale = transform.localScale.x;
		cameraObject = Camera.main;
		prevYpos = getScreenPos ();
		setPlayerScale ();
	}

	// Update is called once per frame
	void Update () {
		setScaleDirection ();
		setPlayerScale ();
	}
}
