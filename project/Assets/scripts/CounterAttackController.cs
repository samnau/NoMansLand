using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackController : MonoBehaviour {
    public bool canCounter = false;
    public float counterWindow = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
    public void OpenCounterWindow()
    {
        canCounter = true;
        StartCoroutine(ControlCounterWindow());
    }

    IEnumerator ControlCounterWindow()
    {
        yield return new WaitForSeconds(counterWindow);
        canCounter = false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
