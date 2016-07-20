using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakerMotionController : MonoBehaviour {
	private float moveDistance = 10.0f;
	private float moveTime = 0.75f;
	public string direction;
	private int directionModifier;
	private Vector2 hideVector;
	private Vector2 showVector;
	private float showPositionX;
	private Vector2 startVector;
	private Vector2 endVector;
	private bool rewind = false;
	private bool startMovement = false;
	// Use this for initialization
	void Start () {
		directionModifier = direction == "left" ? -1 : 1;
		hideVector = transform.position;
		showPositionX = hideVector.x + moveDistance * directionModifier;
		showVector = new Vector2 (showPositionX, hideVector.y);
	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){
		//CheckForSpaceBar();
	}
/*	
	IEnumerator WaitForMovementTrigger()
	{
		do
		{
			yield return null;
		} while (!startMovement);
	}*/
	public void ToggleMovement(){
		setMoveVectors();
		StartCoroutine(MoveOverSeconds());
	}
	void CheckForSpaceBar(){
		if(Input.GetKeyDown(KeyCode.Space)){
			ToggleMovement();
		}
	}
	IEnumerator WaitForSpaceBar()
	{
		do
		{
			yield return null;
		} while (!Input.GetKeyDown(KeyCode.Space));
		startMovement = true;
	}

	void setMoveVectors (){
		startVector = rewind ? showVector : hideVector;
		endVector = rewind ? hideVector : showVector;
	}
	IEnumerator MoveOverSeconds ()
	{
		float elapsedTime = 0;
		while (elapsedTime < moveTime)
		{
			transform.position = Vector2.Lerp(startVector, endVector, (elapsedTime / moveTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		startMovement = false;
		rewind = !rewind;
	}
}
