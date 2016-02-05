using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {
	public GameObject Player;
	private float X;
	// Use this for initialization
	void Start () {
		X = transform.localScale.x;
	}
	private bool isMoving(){
		return Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right");
	}
	private bool isMovingRight(){
		return Input.GetKey("right");
	}

	public float motionDistance = 1.0f;
	// Update is called once per frame
	void Update () {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * motionDistance * Time.deltaTime;

		if (Input.GetAxis("Horizontal") < 0)
		{
			transform.localScale = new Vector3(X,transform.localScale.y,transform.localScale.z);
		}
		else if (Input.GetAxis("Horizontal") > 0)
		{
			transform.localScale = new Vector3(-X,transform.localScale.y,transform.localScale.z);
		}

		//if (isMoving()) {
		//	GetComponent<Animator>().SetBool("isWalking",true);
		//} else {
		//	GetComponent<Animator>().SetBool("isWalking",false);
		//}
	}

}
