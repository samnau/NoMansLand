using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public bool damage = false;
    DefenseController DefenseController;
    Transform targetParent;
	void Start () {
        targetParent = gameObject.transform.parent;
        DefenseController = targetParent.GetComponentInChildren<DefenseController>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            damage = true;
            targetParent.GetComponent<HealthController>().TakeDamage();
            DefenseController.defense = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            damage = false;
        }
    }
}
