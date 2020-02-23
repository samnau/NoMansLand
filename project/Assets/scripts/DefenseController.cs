using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour {
    public bool defense = false;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.name == "attack")
        {
            Debug.Log("defend!");
            defense = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.name == "attack")
        {
            Debug.Log("defense reset!");
            defense = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
