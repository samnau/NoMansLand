using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Wall_Controller : MonoBehaviour {
    private GameObject castleWalls;
	// Use this for initialization
	void Start () {
		castleWalls = GameObject.Find("walls_wrapper");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            castleWalls.GetComponent<Animator>().SetBool("lowerWalls", true);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
