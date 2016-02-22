using UnityEngine;
using System.Collections;

//private Animator animationController;

public class Dialog_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TriggerSpeaker ("LEFT");
		TriggerSpeaker ("RIGHT");
	}
	void TriggerSpeaker(string speakerTarget){
		Animator targetAnimator;
		GameObject targetObject;
		string tagString;
		string direction = speakerTarget == "RIGHT" ? "left" : "right";
		tagString = "Speaker_" + speakerTarget;
		targetObject = GameObject.FindGameObjectWithTag (tagString);
		targetAnimator = targetObject.GetComponent<Animator> ();
		targetAnimator.SetBool (direction, true);
	}

	// Update is called once per frame
	void Update () {

	}
}
