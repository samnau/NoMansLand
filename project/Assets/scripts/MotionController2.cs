using UnityEngine;
using System.Collections;

public class MotionController2 : MonoBehaviour {
	public GameObject Player;
	private float X;
	private string lastKeyPressed;
	private string currentKeyDown;
	string lastKeyReleased;
	private Animator animationController;
	private bool moveKeyReleased;
	private bool moveKeyPressed;
	bool directionHasChanged = true;
	bool isNotColliding = true;

	void OnCollisionEnter2D( Collision2D collision ){
		lastKeyPressed = setCurrentKeyPressed ();
		isNotColliding = false;
	}
	void OnCollisionStay2D( Collision2D collision ){
		setCurrentKeyPressed ();
		isNotColliding = false;
		directionHasChanged = lastKeyPressed != currentKeyDown;
		if (directionHasChanged) {
			isNotColliding = directionHasChanged;
		}
	}
	void OnCollisionExit2D( Collision2D collision ){
		isNotColliding = true;
	}

	void Start () {
		X = transform.localScale.x;
		animationController = GetComponent<Animator> ();
	}
	private void setLastKeyPressed(){
		if (Input.GetKeyDown ("left")) {
			lastKeyPressed = "left";
		}
		if (Input.GetKeyDown ("right")) {
			lastKeyPressed = "right";
		}
		if (Input.GetKeyDown ("up")) {
			lastKeyPressed = "up";
		}
		if (Input.GetKeyDown ("down")) {
			lastKeyPressed = "down";
		}

	}
	private string setCurrentKeyPressed(){
		if (Input.GetKey ("left")) {
			currentKeyDown = "left";
		}
		if (Input.GetKey ("right")) {
			currentKeyDown = "right";
		}
		if (Input.GetKey ("up")) {
			currentKeyDown = "up";
		}
		if (Input.GetKey ("down")) {
			currentKeyDown = "down";
		}

		return currentKeyDown;
	}
	private void setLastKeyReleased(){
		if (Input.GetKey ("left")) {
			lastKeyReleased = "left";
		}
		if (Input.GetKey ("right")) {
			lastKeyReleased = "right";
		}
		if (Input.GetKey ("up")) {
			lastKeyReleased = "up";
		}
		if (Input.GetKey ("down")) {
			lastKeyReleased = "down";
		}
		if (!isMoving ()) {
			currentKeyDown = "";
		}
	}

	private bool isMoving(){
		moveKeyPressed = Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right");
		return moveKeyPressed;
	}
	private bool hasStoppedMovingCheck(){
		return Input.GetKeyUp("up") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("right");
	}
	private bool isMovingRight(){
		return Input.GetKey("right");
	}

	private bool isMovingLeft(){
		return Input.GetKey("left");
	}
	private bool isMovingUp(){
		return Input.GetKey("up");
	}
	private bool isMovingDown(){
		return Input.GetKey("down");
	}

	public float motionDistance = 1.0f;

	void Update () {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if (isNotColliding && directionHasChanged) {
			transform.position += move * motionDistance * Time.deltaTime;
		}
			
		/*if (Input.GetAxis("Horizontal") < 0)
		{
			transform.localScale = new Vector3(X,transform.localScale.y,transform.localScale.z);
		}
		else if (Input.GetAxis("Horizontal") > 0)
		{
			transform.localScale = new Vector3(-X,transform.localScale.y,transform.localScale.z);
		}*/
		animationController.SetBool("WALKING",isMoving());
		animationController.SetBool("RIGHT",isMovingRight());
		animationController.SetBool("LEFT",isMovingLeft());
		animationController.SetBool("UP",isMovingUp());
		animationController.SetBool("DOWN",isMovingDown());

	}

}
