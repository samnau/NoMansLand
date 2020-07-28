using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public bool damage = false;
    public bool damageDefended = false;
    DefenseController DefenseController;
    Transform targetParent;
    BattleCombos BattleCombos;
    BattleKeyCombos battleKeyCombos;
    TimeController TimeController;

    void Start () {
        targetParent = gameObject.transform.parent;
        DefenseController = targetParent.GetComponentInChildren<DefenseController>();
        TimeController = GameObject.FindGameObjectWithTag("familiar").GetComponent<TimeController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetGameObject = collision.gameObject;
        battleKeyCombos = targetGameObject.GetComponent<BattleKeyCombos>();
        var activeAttack = battleKeyCombos.activeAttack;
        if (collision.gameObject.tag == "attack" && activeAttack && !damageDefended)
        {
            Debug.Log("damage collision? "+ damageDefended);
           damage = true;
           targetParent.GetComponent<HealthController>().TakeDamage();
           TimeController.UnFreezeTime();
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
