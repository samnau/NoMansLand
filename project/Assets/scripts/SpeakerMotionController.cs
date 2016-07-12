using UnityEngine;
using System.Collections;


public class SpeakerMotionController : MonoBehaviour {
	private Vector2 speaker_vector;

	private GameObject speaker;
	private Rigidbody2D targetObject;
	private float moveDistance = 1.0f;
	public string direction;
	// Use this for initialization
	void Start () {
		speaker = GameObject.Find("Speaker");
		targetObject = speaker.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate(){
		targetObject.AddRelativeForce (Vector2.right * moveDistance);

	}
}
