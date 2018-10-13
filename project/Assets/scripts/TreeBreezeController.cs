using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBreezeController : MonoBehaviour {
    private Animator animatonController;
    public float breezeDelay = 0f;

	// Use this for initialization
	void Start () {
        animatonController = GetComponent<Animator>();
        StartCoroutine(TreeSequence());
    }

    void controlWind(bool wind)
    {
        animatonController.SetBool("wind", wind);
    }

    void controlWindStrength(bool strong)
    {
        var windSpeed = strong ? 2.0f : 1.0f;
        animatonController.SetBool("strong", strong);
        animatonController.speed = windSpeed;
    }

    IEnumerator TreeSequence()
    {
        yield return new WaitForSeconds(breezeDelay);
        //animatonController.speed = 2.0f;
        controlWind(true);
        yield return new WaitForSeconds(2.0f);
        controlWindStrength(true);
        yield return new WaitForSeconds(5.0f);
        controlWindStrength(false);
        yield return new WaitForSeconds(1.0f);
        controlWind(false);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
