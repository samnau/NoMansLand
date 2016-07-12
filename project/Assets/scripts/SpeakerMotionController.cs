using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakerMotionController : MonoBehaviour {
	private GameObject speaker;
	private float moveDistance = 5.0f;
	public string direction;
	private int directionModifier;
	private Vector2 targetPosition;
	private float startPositionY;
	private float endPositionX;
	// Use this for initialization
	void Start () {
		speaker = GameObject.Find("speaker");
		targetPosition = speaker.transform.position;
		directionModifier = direction == "left" ? -1 : 1;
		startPositionY = targetPosition.y;
		endPositionX = targetPosition.x + moveDistance * directionModifier;

		TriggerMovement();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		//targetObject.AddRelativeForce (Vector2.right * moveDistance);

	}
	void TriggerMovement(){
		var endVector = new Vector2 (endPositionX, startPositionY);
		StartCoroutine(MoveOverSeconds(speaker,endVector,1.0f));
	}
	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector2 end, float seconds)
	{
		float elapsedTime = 0;
		Vector2 startingPos = objectToMove.transform.position;
		while (elapsedTime < seconds)
		{
			transform.position = Vector2.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = end;
	}
}
