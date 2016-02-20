using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (Rigidbody))]
public class ExampleScript : MonoBehaviour {
	GameObject myGameObject;
	Transform myTransform;
	private Rigidbody myRigidBody;
	// Use this for initialization
	void Start () {
		//myRigidBody = GetComponent<Rigidbody> ();
		myGameObject = GameObject.Find("Jack");
		myTransform = myGameObject.GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		myTransform.position = Vector3.up * Time.time;
		//myTransform.position= Vector3.up * Time.time;
		//myRigidBody.AddForce(Vector3.up * 50);
	}
}
