using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackController : MonoBehaviour {
    public bool canCounter = false;
    public float counterWindow = 1.5f;
	
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
}
