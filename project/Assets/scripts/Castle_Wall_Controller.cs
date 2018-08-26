using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Wall_Controller : MonoBehaviour {
    private GameObject castleWalls;
    private Camera mainCamera;
    private bool wallsAreUp = true;
	// Use this for initialization
	void Start () {
		castleWalls = GameObject.Find("walls_wrapper");
        mainCamera = Camera.main;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && wallsAreUp)
        {
            castleWalls.GetComponent<Animator>().SetBool("lowerWalls", true);
            mainCamera.GetComponent<Camera_Shaker>().enabled = true;
            StartCoroutine(WallSequence());
        }
    }

    IEnumerator WallSequence()
    {
        Debug.Log("next");
        var hero = GameObject.FindGameObjectWithTag("Player");
        //hero.GetComponent<PlayerMotionController>().enabled = false;
        //hero.GetComponent<InputStateTracker>().isWalking = false;
        yield return new WaitForSeconds(2.0f);
        wallsAreUp = false;
        //hero.GetComponent<PlayerMotionController>().enabled = true;
        hero.GetComponent<Perspective_Scale_Controller>().enabled = false;
        GameObject.Find("wall_blocker").GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
