using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour {
    bool canDefend = false;
    bool takeDamage = false;
    bool attackDefended = false;
    DefenseController DefenseController;
    DamageController DamageController;
    CounterAttackController CounterAttackController;
    Key_Validator Key_Validator;
    public string[] defenseCombo;
    public string[] counterAttackCombo;

	// Use this for initialization
	void Start () {
       defenseCombo = new string[] { "", "" };
        DefenseController = GameObject.Find("defense_window").GetComponent<DefenseController>();
        DamageController = GameObject.Find("damage_zone").GetComponent<DamageController>();
        CounterAttackController = GetComponent<CounterAttackController>();
        Key_Validator = GetComponent<Key_Validator>();
       // Key_Validator.keyCombo = new string[] { "w", "up" };
    }
	
    IEnumerator CheckDefenseSuccess()
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("defended: " + attackDefended);
    }

    // Update is called once per frame
    void Update () {
        canDefend = DefenseController.defense;
        takeDamage = DamageController.damage;

        if(canDefend && !takeDamage)
        {
            Key_Validator.keyCombo = defenseCombo;
            attackDefended = Key_Validator.comboPressed;
            StartCoroutine(CheckDefenseSuccess());
        }


        if (Input.anyKeyDown)
        {
            Debug.Log(canDefend);

        }
    }
}
