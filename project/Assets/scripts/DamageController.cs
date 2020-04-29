using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public bool damage = false;
    public bool damageDefended = false;
    DefenseController DefenseController;
    Transform targetParent;
    BattleCombos BattleCombos;

    void Start () {
        targetParent = gameObject.transform.parent;
        DefenseController = targetParent.GetComponentInChildren<DefenseController>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetGameObject = collision.gameObject;
        BattleCombos = targetGameObject.GetComponent<BattleCombos>();
        var activeAttack = BattleCombos.activeAttack;
        if (collision.gameObject.tag == "attack" && activeAttack && !damageDefended)
        {
            Debug.Log("damage collision");
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
