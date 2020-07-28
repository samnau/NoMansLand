using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour {
    public bool defense = false;
    string[] defenseCombo;
    string[] counterAttackCombo;
    KeyCode[] defenseKeyCombo;
    KeyCode[] counterAttackKeyCombo;
    CounterAttackController CounterAttackController;
    FightController FightController;
    BattleCombos BattleCombos;
    BattleKeyCombos battleKeyCombos;
    TimeController TimeController;
    int collisionCount= 0;

    void Start () {
        CounterAttackController = GetComponent<CounterAttackController>();
        FightController = GetComponentInParent<FightController>();
        TimeController = GameObject.FindGameObjectWithTag("familiar").GetComponent<TimeController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        var targetGameObject = collision.gameObject;
        BattleCombos = targetGameObject.GetComponent<BattleCombos>();
        battleKeyCombos = targetGameObject.GetComponent<BattleKeyCombos>();
        var activeAttack = BattleCombos.activeAttack;
        if (targetGameObject.tag == "attack" && activeAttack && collisionCount == 0)
        {
            TimeController.SlowTime();
            collisionCount++;
            //defenseCombo = BattleCombos.defenseCombo;
            //counterAttackCombo = BattleCombos.counterAttackCombo;
            //FightController.defenseCombo = defenseCombo;
            //FightController.counterAttackCombo = counterAttackCombo;

            defenseKeyCombo = battleKeyCombos.defenseCombo;
            counterAttackKeyCombo = battleKeyCombos.counterAttackCombo;
            FightController.defenseKeyCombo = defenseKeyCombo;
            FightController.counterAttackKeyCombo = counterAttackKeyCombo;
            
            Debug.Log("defend now");
            defense = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            var targetGameObject = collision.gameObject;
            BattleCombos = targetGameObject.GetComponent<BattleCombos>();
            battleKeyCombos = targetGameObject.GetComponent<BattleKeyCombos>();
            //BattleCombos.activeAttack = true;
            Debug.Log("attack exited");
            defense = false;
        }
    }
}
