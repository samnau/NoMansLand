using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour {
    bool canDefend = false;
    bool takeDamage = false;
    bool attackDefended = false;
    DefenseController DefenseController;
    DamageController DamageController;
    Key_Validator Key_Validator;

	// Use this for initialization
	void Start () {
        DefenseController = GameObject.Find("defense_window").GetComponent<DefenseController>();
        DamageController = GameObject.Find("damage_zone").GetComponent<DamageController>();
        Key_Validator = GetComponent<Key_Validator>();
        Key_Validator.keyCombo = new string[] { "w", "up" };
    }
	
	// Update is called once per frame
	void Update () {
        canDefend = DefenseController.defense;
        takeDamage = DamageController.damage;

        if(canDefend && !takeDamage)
        {
            attackDefended = Key_Validator.comboPressed;
        }


        if (Input.anyKeyDown)
        {
            Debug.Log(canDefend);
        }
    }
}
