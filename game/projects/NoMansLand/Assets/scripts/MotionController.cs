using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {
	public GameObject Player;
	// Use this for initialization
	void Start () {

	}
	public float motionDistance = 1.0f;
	// Update is called once per frame
	void Update () {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * motionDistance * Time.deltaTime;

		if (Input.anyKey) {
			animation.Play ("player_test_animation2");
		} else {
			animation.Play ("player_default_animation");
		}
	}

}
