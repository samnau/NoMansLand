using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakerMotionController : MonoBehaviour {
	private GameObject speaker;
	private float moveDistance = 5.0f;
	private float moveTime = 1.0f;
	public string direction;
	private int directionModifier;
	private Vector2 targetPosition;
	private float startPositionY;
	private float endPositionX;
	private Vector2 endVector;
	// Use this for initialization
	void Start () {
		speaker = GameObject.Find("speaker");
		targetPosition = speaker.transform.position;
		directionModifier = direction == "left" ? -1 : 1;
		startPositionY = targetPosition.y;
		endPositionX = targetPosition.x + moveDistance * directionModifier;
		endVector = new Vector2 (endPositionX, startPositionY);
		TriggerMovement();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		//targetObject.AddRelativeForce (Vector2.right * moveDistance);

	}
	void TriggerMovement(){
		StartCoroutine(MoveOverSeconds());
	}
	public IEnumerator MoveOverSeconds ()
	{
		float elapsedTime = 0;
		Vector2 startingPos = speaker.transform.position;
		while (elapsedTime < moveTime)
		{
			transform.position = Vector2.Lerp(startingPos, endVector, (elapsedTime / moveTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = endVector;
	}
}
