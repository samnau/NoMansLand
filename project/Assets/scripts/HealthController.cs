using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    public int health = 3;
    public bool isDead = false;
    bool deadFlagForTest = false;
	// Use this for initialization
	void Start () {
		
	}
	public void TakeDamage()
    {
        health--;
    }

    bool CheckForDeath()
    {
        return health < 1;
    }
	// Update is called once per frame
	void Update () {
        isDead = CheckForDeath();

        if (isDead && !deadFlagForTest)
        {
            Debug.Log("this one is toast!");
            deadFlagForTest = true;
        }
    }
}
