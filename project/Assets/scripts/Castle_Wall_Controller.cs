﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Wall_Controller : MonoBehaviour {
    private GameObject castleWalls;
    private Camera mainCamera;
    private bool wallsAreUp = true;
    private GameObject[] treeArray;
	// Use this for initialization
	void Start () {
		castleWalls = GameObject.Find("walls_wrapper");
        mainCamera = Camera.main;
        treeArray = GameObject.FindGameObjectsWithTag("tree");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && wallsAreUp)
        {
            //castleWalls.GetComponent<Animator>().SetBool("lowerWalls", true);
            //mainCamera.GetComponent<Camera_Shaker>().enabled = true;
            foreach(GameObject treeItem in treeArray)
            {
                treeItem.GetComponent<Animator>().speed = 2.0f;
            }
            startStrongWind();
            StartCoroutine(revealTheSeal());
            //StartCoroutine(bringTheWallsDown());
        }
    }
    IEnumerator revealTheSeal()
    {
        var seal = GameObject.Find("castle_seal_wrapper");
        seal.GetComponent<Animator>().SetBool("reveal", true);
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(bringTheWallsDown());
    }
    IEnumerator bringTheWallsDown()
    {
        castleWalls.GetComponent<Animator>().SetBool("lowerWalls", true);
        mainCamera.GetComponent<Camera_Shaker>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(WallSequence());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // re-enable the perspective controller, after resizing the trigger to cover the bridge
    }

    private void startStrongWind()
    {
        foreach(GameObject treeItem in treeArray)
        {
            var breezeController = treeItem.GetComponent<TreeBreezeController>();
            breezeController.startStrongWind();
        }
    }
    private void turnOffWind ()
    {
        Debug.Log("wind off");
        foreach (GameObject treeItem in treeArray)
        {
            var breezeController = treeItem.GetComponent<TreeBreezeController>();
            breezeController.stopAllWind();
        }
    }
    IEnumerator WallSequence()
    {
        var hero = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(2.0f);
        wallsAreUp = false;
        //hero.GetComponent<PlayerMotionController>().enabled = true;
        hero.GetComponent<Perspective_Scale_Controller>().enabled = false;
        GameObject.Find("wall_blocker").GetComponent<BoxCollider2D>().enabled = false;
        turnOffWind();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
