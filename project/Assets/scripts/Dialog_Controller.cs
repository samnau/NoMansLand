using UnityEngine;
using System.Collections;

//private Animator animationController;

public class Dialog_Controller : MonoBehaviour {
	private Animator animationController;
	public GameObject speakerLeft;
	// Use this for initialization
	void Start () {
		//animationController = GetComponent<Animator> ();
		//animationController.SetBool("right",true);
		speakerLeft.GetComponent<Animator> ().SetBool("right",true); 
		 //Debug.Log(speaker);
		 //.GetComponent.<Animator>().setBool("right", true);
	}

	// Update is called once per frame
	void Update () {

	}
}
