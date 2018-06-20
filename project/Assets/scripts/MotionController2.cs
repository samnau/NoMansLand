using UnityEngine;
using System.Collections;

public class MotionController2 : MonoBehaviour {
	public GameObject Player;
	private float X;
	private string lastKeyPressed = "LEFT";
	private string currentKeyDown;
	string lastKeyReleased = "LEFT";
	private Animator animationController;
	private bool moveKeyReleased;
	private bool moveKeyPressed;
	bool directionHasChanged = true;
	bool isNotColliding = true;
	Rigidbody2D myRigidBody2D;

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
		myRigidBody2D = GetComponent<Rigidbody2D> ();
	}
	private void startMovement(){
		var horizontalValue = Input.GetAxis ("Horizontal") * motionDistance;
		var verticalValue = Input.GetAxis ("Vertical") * motionDistance;
		var walkingVelocityReached = Mathf.Abs (horizontalValue) > 0.5 || Mathf.Abs (verticalValue) > 0.5;;

		if (walkingVelocityReached) {
			//myRigidBody2D.velocity = new Vector2(horizontalValue,myRigidBody2D.velocity.y);
			myRigidBody2D.velocity = new Vector2(horizontalValue,verticalValue);
		}
	}
	private void moveRight(){
		myRigidBody2D.velocity = Vector2.right;
	}
	private void moveUp(){
		myRigidBody2D.velocity = Vector2.up;
	}
	private void moveDown(){
		myRigidBody2D.velocity = Vector2.down;
	}
	void checkForSpaceBar(){
		var spacebarDown = Input.GetKey (KeyCode.Space);
		//if (Input.GetKey (KeyCode.Space)) {
		animationController.SetBool("ACTION", spacebarDown);
		//}
	}
//	private void startMovement(){
//		if (isMovingLeft () || isMovingRight()) {
//			moveHorizontal ();
//		}
//	}
	private void stopMovement(){
		myRigidBody2D.velocity = Vector2.zero;
	}
	private void setLastKeyPressed(){
		if (Input.GetKeyDown ("left")) {
			lastKeyPressed = "LEFT";
		}
		if (Input.GetKeyDown ("right")) {
			lastKeyPressed = "RIGHT";
		}
		if (Input.GetKeyDown ("up")) {
			lastKeyPressed = "UP";
		}
		if (Input.GetKeyDown ("down")) {
			lastKeyPressed = "DOWN";
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
		if (Input.GetKeyUp ("left")) {
			lastKeyReleased = "LEFT";
		}
		if (Input.GetKeyUp ("right")) {
			lastKeyReleased = "RIGHT";
		}
		if (Input.GetKeyUp ("up")) {
			lastKeyReleased = "UP";
		}
		if (Input.GetKeyUp ("down")) {
			lastKeyReleased = "DOWN";
		}
//		if (!isMoving ()) {
//			currentKeyDown = "";
//		}
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
	void setAnimationStates(){
		animationController.SetBool("WALKING",isMoving());
		animationController.SetBool(lastKeyPressed,true);

		if(lastKeyPressed=="RIGHT"){
			animationController.SetBool("LEFT",false);
			animationController.SetBool("UP",false);
			animationController.SetBool("DOWN",false);
		}
		if(lastKeyPressed=="LEFT"){
			animationController.SetBool("RIGHT",false);
			animationController.SetBool("UP",false);
			animationController.SetBool("DOWN",false);
		}
		if(lastKeyPressed=="UP"){
			animationController.SetBool("RIGHT",false);
			animationController.SetBool("LEFT",false);
			animationController.SetBool("DOWN",false);
		}
		if(lastKeyReleased=="DOWN"){
			animationController.SetBool("RIGHT",false);
			animationController.SetBool("LEFT",false);
			animationController.SetBool("UP",false);
		}
		checkForAdditionalInput();
	}
	void checkForAdditionalInput(){
		var horizontalValue = Input.GetAxis ("Horizontal") * motionDistance;
		var verticalValue = Input.GetAxis ("Vertical") * motionDistance;
		var movingLeft = horizontalValue<0;
		var movingRight = horizontalValue>0;
		var movinUp = verticalValue<0;
		var movingDown = verticalValue>0;

		var movingHorizontal = horizontalValue != 0;
		var movingVertical = verticalValue !=0;
		if(movingVertical || movingHorizontal){
			if(movingHorizontal){
				animationController.SetBool("LEFT",movingLeft);
				animationController.SetBool("RIGHT",movingRight);
			}
			if(movingVertical){
				animationController.SetBool("UP",movinUp);
				animationController.SetBool("DOWN",movingDown);
			}
		}
	}
	void collisionMovementCheck(){
		if (isNotColliding && directionHasChanged) {
			startMovement();
			if(!isMoving()){
				stopMovement ();
			}
		}
	}
	void FixedUpdate () {
		setLastKeyPressed ();
		collisionMovementCheck ();
		setAnimationStates ();
		checkForSpaceBar ();
		//preserving old movement method for future reference
		// var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		// transform.position += move * motionDistance * Time.deltaTime;
	}

}
