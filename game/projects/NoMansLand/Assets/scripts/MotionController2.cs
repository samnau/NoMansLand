using UnityEngine;
using System.Collections;

public class MotionController2 : MonoBehaviour {
	public GameObject Player;
	private float X;
	private string lastKeyPressed;
	private Animator animationController;
	private bool moveKeyReleased;
	private bool moveKeyPressed;
	// Use this for initialization
	void Start () {
		X = transform.localScale.x;
		animationController = GetComponent<Animator> ();
	}
		private void setLastKeyPressed(){
				if (Input.GetKeyDown ("left")) {
						lastKeyPressed = "left";
				}
		}
	private bool isMoving(){
		//moveKeyReleased = Input.GetKeyUp("up") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("right");
		moveKeyPressed = Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right");
		return moveKeyPressed;
	}
	private void isMovingCheck(){
		moveKeyReleased = Input.GetKeyUp("up") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("right");
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
	// Update is called once per frame
	void Update () {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * motionDistance * Time.deltaTime;

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
				print ("moving=");
				Debug.Log (isMoving());
				print ("down=");
				Debug.Log (isMovingDown());
		//if (isMoving()) {
		//	GetComponent<Animator>().SetBool("WALKING",true);
			//GetComponent<Animator>().SetBool("isWalking",true);
		//} else {
		//	GetComponent<Animator>().SetBool("isWalking",false);
		//}
	}

}
