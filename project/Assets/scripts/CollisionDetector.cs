using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {
    GameObject hit_signal;
	// Use this for initialization
	void Start () {
        hit_signal = GameObject.Find("hit_signal");
        hit_signal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.name == "attack")
        {
            Debug.Log("hit!");
            hit_signal.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.name == "attack")
        {
            Debug.Log("done!");
            hit_signal.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
