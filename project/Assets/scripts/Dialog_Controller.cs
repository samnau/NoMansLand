using UnityEngine;
using System.Collections;

//private Animator animationController;

public class Dialog_Controller : MonoBehaviour {
private Animator animationController;
	// Use this for initialization
	void Start () {
		animationController = GetComponent<Animator> ();
		animationController.SetBool("right",true);
	}

	// Update is called once per frame
	void Update () {

	}
}
