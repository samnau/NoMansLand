using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBreezeController : MonoBehaviour {
    private Animator animatonController;
	// Use this for initialization
	void Start () {
        animatonController = GetComponent<Animator>();
        StartCoroutine(TreeSequence());
    }
    IEnumerator TreeSequence()
    {
        yield return new WaitForSeconds(3.0f);
        animatonController.speed = 2.0f;
        yield return new WaitForSeconds(5.0f);
        animatonController.speed = 1.0f;
        animatonController.SetBool("strong", false);
        yield return new WaitForSeconds(1.0f);
        animatonController.SetBool("wind", false);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
