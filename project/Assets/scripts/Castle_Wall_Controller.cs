using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Wall_Controller : MonoBehaviour {
    private GameObject castleWalls;
    private Camera mainCamera;
	// Use this for initialization
	void Start () {
		castleWalls = GameObject.Find("walls_wrapper");
        mainCamera = Camera.main;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            castleWalls.GetComponent<Animator>().SetBool("lowerWalls", true);
            mainCamera.GetComponent<Camera_Shaker>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
