using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour {
    public bool defense = false;
    string[] defenseCombo;
    string[] counterAttackCombo;
    CounterAttackController CounterAttackController;
    FightController FightController;
    BattleCombos BattleCombos;

    void Start () {
        CounterAttackController = GetComponent<CounterAttackController>();
        FightController = GetComponentInParent<FightController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetGameObject = collision.gameObject;
        if (targetGameObject.tag == "attack")
        {
            BattleCombos = targetGameObject.GetComponent<BattleCombos>();
            defenseCombo = BattleCombos.defenseCombo;
            counterAttackCombo = BattleCombos.counterAttackCombo;
            FightController.defenseCombo = defenseCombo;
            FightController.counterAttackCombo = counterAttackCombo;
            defense = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            defense = false;
        }
    }
}
