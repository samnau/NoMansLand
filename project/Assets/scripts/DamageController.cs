using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public bool damage = false;
    DefenseController DefenseController;
    Transform targetParent;
	// Use this for initialization
	void Start () {
        targetParent = gameObject.transform.parent;
        DefenseController = targetParent.GetComponentInChildren<DefenseController>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack")
        {
          //  Debug.Log("hit!");
            damage = true;
            targetParent.GetComponent<HealthController>().TakeDamage();
            DefenseController.defense = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack")
        {
          //  Debug.Log("done!");
            damage = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
