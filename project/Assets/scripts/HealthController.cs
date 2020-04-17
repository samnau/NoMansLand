using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    public int health = 1;
    public bool isDead = false;
    bool deadFlagForTest = false;
    public bool takeDamageFromCollision = false;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack" && takeDamageFromCollision)
        {
            //  Debug.Log("hit!");
            TakeDamage();
        }
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
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.25f, 0, 0.5f);
            deadFlagForTest = true;
        }
    }
}
