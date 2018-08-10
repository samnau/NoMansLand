using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack_Order_Controller : MonoBehaviour {
	//private float Xpos;
	private float Ypos;
	void StackSetter(){
		//Xpos = transform.position.x;
		Ypos = transform.position.y;
        var objectSorter = GetComponent<SpriteRenderer>();
        var newOrder = Mathf.RoundToInt(Ypos * -100f);
        objectSorter.sortingOrder = newOrder;
        //transform.position = new Vector3(Xpos, Ypos, Ypos);
	}

	void Start () {
		StackSetter ();
	}
	// Update is called once per frame
	void Update () {
		//StackSetter();
	}
}
