using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public bool damage = false;
    DefenseController DefenseController;
	// Use this for initialization
	void Start () {
        DefenseController = gameObject.transform.parent.GetComponentInChildren<DefenseController>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.name == "attack")
        {
            Debug.Log("hit!");
            damage = true;
            DefenseController.defense = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.name == "attack")
        {
            Debug.Log("done!");
            damage = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
