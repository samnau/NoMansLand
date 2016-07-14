using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakerMotionController : MonoBehaviour {
	private float moveDistance = 5.0f;
	private float moveTime = 0.5f;
	public string direction;
	private int directionModifier;
	private Vector2 targetPosition;
	private float startPositionX;
	private float startPositionY;
	private Vector2 startVector;
	private float endPositionX;
	private Vector2 endVector;
	private bool slideOut = false;
	private bool slideIn = false;
	private bool startMovement = false;
	// Use this for initialization
	void Start () {
		targetPosition = transform.position;
		directionModifier = direction == "left" ? -1 : 1;
		startPositionX = targetPosition.x;
		startPositionY = targetPosition.y;
		startVector = new Vector2 (startPositionX,startPositionY);
		endPositionX = targetPosition.x + moveDistance * directionModifier;
		endVector = new Vector2 (endPositionX, startPositionY);
		StartCoroutine(TriggerMovement());
	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){
		//targetObject.AddRelativeForce (Vector2.right * moveDistance);

	}
	IEnumerator TriggerMovement(){
		yield return StartCoroutine(WaitForSpaceBar());
		yield return StartCoroutine(WaitForMovementTrigger());
		yield return StartCoroutine(MoveOverSeconds());
		yield return StartCoroutine(WaitForSpaceBar());
		yield return StartCoroutine(WaitForMovementTrigger());
		yield return StartCoroutine(RewindMotion());
	}
	IEnumerator WaitForMovementTrigger()
	{

		do
		{
			yield return null;
		} while (!startMovement);
	}
	IEnumerator WaitForSpaceBar()
	{
		do
		{
			yield return null;
		} while (!Input.GetKeyDown(KeyCode.Space));
		startMovement = true;
	}
	IEnumerator WaitForEnter()
	{
		do
		{
			yield return null;
		} while (!Input.GetKeyDown(KeyCode.Return));
	}

	IEnumerator RewindMotion ()
	{
		startVector = transform.position;
		endVector = new Vector2 (startPositionX, startPositionY);
		yield return StartCoroutine(MoveOverSeconds());
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
		startVector = transform.position = endVector;
		endVector = new Vector2 (startPositionX, startPositionY);
	}
}
