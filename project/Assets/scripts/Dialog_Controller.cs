using UnityEngine;
using System.Collections;

//private Animator animationController;

public class Dialog_Controller : MonoBehaviour {
	Animator animator_left;
	Animator animator_right;
	GameObject speakerLeft;
	GameObject speakerRight;
	// Use this for initialization
	void Start () {
		speakerLeft = GameObject.FindGameObjectWithTag ("Speaker_LEFT");
		speakerRight = GameObject.FindGameObjectWithTag ("Speaker_RIGHT");
		animator_left = speakerLeft.GetComponent<Animator> ();
		animator_left.SetBool ("right", true);
		animator_right = speakerRight.GetComponent<Animator> ();
		animator_right.SetBool ("left", true);

		//animationController = GetComponent<Animator> ();
		//animationController.SetBool("right",true);
		//speakerLeft.GetComponent<Animator> ().SetBool("right",true); 
		 //Debug.Log(speaker);
		 //.GetComponent.<Animator>().setBool("right", true);
	}

	// Update is called once per frame
	void Update () {

	}
}
