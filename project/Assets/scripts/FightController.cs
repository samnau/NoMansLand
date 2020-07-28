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
    GameObject Monster;
    BlockedAttackController blockedAttackController;
    CounterAttackController CounterAttackController;
    GameObject FireShield;
    Animator FireShieldAnimator;
    TimeController TimeController;
    Key_Validator Key_Validator;
    KeyCodeValidator KeyCodeValidator;
    public string[] defenseCombo;
    public string[] counterAttackCombo;
    public KeyCode[] defenseKeyCombo;
    public KeyCode[] counterAttackKeyCombo;
    bool attackCycleInProgress = false;
    // for testing only
    bool secondAttackTime = false;

	// Use this for initialization
	void Start () {
        //defenseCombo = new string[] { "", "" };
        defenseKeyCombo = new KeyCode[] { KeyCode.Underscore, KeyCode.Underscore };
        DefenseWindow = GameObject.Find("defense_window");
        DefenseController = DefenseWindow.GetComponent<DefenseController>();
        DamageController = GameObject.Find("damage_zone").GetComponent<DamageController>();
        CounterAttackController = DefenseWindow.GetComponent<CounterAttackController>();
        TimeController = GameObject.FindGameObjectWithTag("familiar").GetComponent<TimeController>();
        Monster = GameObject.FindGameObjectWithTag("Enemy");
        blockedAttackController = Monster.GetComponent<BlockedAttackController>();
        FireShield = GameObject.Find("fire_shield_wrapper");
        FireShieldAnimator = FireShield.GetComponent<Animator>();
        //Key_Validator = GetComponent<Key_Validator>();
        KeyCodeValidator = GetComponent<KeyCodeValidator>();
    }
	
    IEnumerator CheckDefenseSuccess()
    {
        yield return new WaitForSeconds(0.1f);
        if(attackDefended)
        {
            SuccessfulDefenseResponse();
            CounterAttackController.OpenCounterWindow();
        }
    }

    void SuccessfulDefenseResponse()
    {
        Debug.Log("Defense Success");
        TimeController.StopTime();
        var hero = GameObject.FindGameObjectWithTag("Player");
        hero.GetComponent<Animator>().SetBool("ACTION", true);
        FireShieldAnimator.SetBool("appear", true);
        //TimeController.UnFreezeTime();
        StartCoroutine("TimeRestart");
       // var attackObject = secondAttackTime ? GameObject.Find("attack_2") : GameObject.Find("attack");
       // var attackObjectRb = attackObject.GetComponent<Rigidbody2D>();
     //   attackObjectRb.gravityScale = 0f;
     //   attackObjectRb.velocity = new Vector2(0, 0);

    }

    IEnumerator TimeRestart()
    {
        yield return new WaitForSeconds(1.75f);
        TimeController.UnFreezeTime();
        blockedAttackController.attackBlocked = true;
    }

    IEnumerator CheckCounterSuccess()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("countered: " + attackCountered);
        if (attackCountered)
        {
            SuccessfulCounterAttack();
        }
        attackCountered = false;
        attackDefended = false;
    }

    void SuccessfulCounterAttack()
    {
        // HERE: trigger animation state and battle status updates, in the appliied version
        // these are just visual debugging to see the key combos working

        //var attackObject = secondAttackTime ? GameObject.Find("attack_2") : GameObject.Find("attack");

       // var attackObjectRb = attackObject.GetComponent<Rigidbody2D>();
        Debug.Log("Counter attack!");
       // var counterForce = new Vector2(1000.0f, 0);
        //attackObjectRb.AddRelativeForce(counterForce);
        // just for testing and fun
       // var attack2 = GameObject.Find("attack_2");
        //attack2.GetComponent<Rigidbody2D>().gravityScale = 0.35f;
       // secondAttackTime = true;
    }

    void DoBattleChecks()
    {
        canDefend = DefenseController.defense;
        takeDamage = DamageController.damage;
        canCounter = CounterAttackController.canCounter;
        if(!attackCycleInProgress)
        {
            watchForDefense();
        }
       
        watchForCounter();
    }

    void watchForDefense()
    {
        if (canDefend && !takeDamage && !attackDefended)
        {
            //Key_Validator.keyCombo = defenseCombo;
            KeyCodeValidator.keyCombo = defenseKeyCombo;
//            attackDefended = Key_Validator.comboPressed;
            attackDefended = KeyCodeValidator.comboPressed;

            DamageController.damageDefended = attackDefended;
            if(attackDefended)
            {
                attackCycleInProgress = true;
            }
            Debug.Log("attack defended: " + attackDefended);
            StartCoroutine(CheckDefenseSuccess());
//            Key_Validator.comboPressed = false;
            KeyCodeValidator.comboPressed = false;
        }
    }

    void watchForCounter()
    {
        if (attackDefended && canCounter)
        {
//            Key_Validator.keyCombo = counterAttackCombo;
            KeyCodeValidator.keyCombo = counterAttackKeyCombo;
//            attackCountered = Key_Validator.comboPressed;
            attackCountered = KeyCodeValidator.comboPressed;
            StartCoroutine(CheckCounterSuccess());
        }
    }

    // Update is called once per frame
    void Update () {
        DoBattleChecks();
    }
}
