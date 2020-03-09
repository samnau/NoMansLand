using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour {
    bool canDefend = false;
    bool takeDamage = false;
    bool attackDefended = false;
    bool attackCountered = false;
    bool canCounter = false;
    GameObject DefenseWindow;
    DefenseController DefenseController;
    DamageController DamageController;
    CounterAttackController CounterAttackController;
    Key_Validator Key_Validator;
    public string[] defenseCombo;
    public string[] counterAttackCombo;

	// Use this for initialization
	void Start () {
       defenseCombo = new string[] { "", "" };
        DefenseWindow = GameObject.Find("defense_window");
        DefenseController = DefenseWindow.GetComponent<DefenseController>();
        DamageController = GameObject.Find("damage_zone").GetComponent<DamageController>();
        CounterAttackController = DefenseWindow.GetComponent<CounterAttackController>();
        Key_Validator = GetComponent<Key_Validator>();
       // Key_Validator.keyCombo = new string[] { "w", "up" };
    }
	
    IEnumerator CheckDefenseSuccess()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("defended: " + attackDefended);
        if(attackDefended)
        {
            CounterAttackController.OpenCounterWindow();
        }
    }

    IEnumerator CheckCounterSuccess()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("countered: " + attackCountered);
        if (attackCountered)
        {
            Debug.Log("Counter attack!");
            attackCountered = false;
            attackDefended = false;
        }
    }

    // Update is called once per frame
    void Update () {
        canDefend = DefenseController.defense;
        takeDamage = DamageController.damage;
        canCounter = CounterAttackController.canCounter;
        if(canDefend && !takeDamage && !attackDefended)
        {
            Key_Validator.keyCombo = defenseCombo;
            attackDefended = Key_Validator.comboPressed;
            StartCoroutine(CheckDefenseSuccess());
        }

        if(attackDefended)
        {
            Key_Validator.keyCombo = counterAttackCombo;
            attackCountered = Key_Validator.comboPressed;
            StartCoroutine(CheckCounterSuccess());
        }

        if (Input.anyKeyDown)
        {
            Debug.Log(canDefend);

        }
    }
}
