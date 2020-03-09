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

    // Use this for initialization
    void Start () {
        CounterAttackController = GetComponent<CounterAttackController>();
        FightController = GetComponentInParent<FightController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetGameObject = collision.gameObject;
        //Debug.Log(collision.name);
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
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack")
        {
          //  Debug.Log("defense reset!");
            defense = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
