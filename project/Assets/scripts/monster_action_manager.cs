using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class monster_action_manager : MonoBehaviour {
    public string current_attack;
    public string defense_key;

    public string[] attack_list;
    public float attack_duration = 2.0f;
    public float defense_window = 1.0f;
    public float defense_start = 1.0f;
    float next_attack_delay;
    public bool validDefense = false;
    bool attackDefended = false;
    bool defenseWindowMissed = false;
    public bool heroCanAttack = false;
    Hero_Health_Value healthTracker;
    MonsterHealthManager monsterHealthManager;
    GameObject healthIndicator;
    GameObject monster;
    Vector2 attackStartPosition;
    Monster monsterClass;
    //List<Monster.ComboKeys> monsterCombos;
    Monster.ComboKeys currentCombo;
    Key_Validator defenseComboChecker;
    private GameObject monsterWrapper;
    public GameObject ball;
    private GameObject test_attack;
    string defenseComboText;
    Text defensePrompt;
    bool monsterIsAlive;

    Hero_Battle_Action_Manager hero_action_manager;

    void Start () {
       monster = GameObject.FindGameObjectWithTag("Enemy");
       attackStartPosition = new Vector2(monster.transform.position.x, monster.transform.position.y + 3.6f);
       hero_action_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero_Battle_Action_Manager>();
       var healthIndicator = GameObject.Find("health_value");
       healthTracker = healthIndicator.GetComponent<Hero_Health_Value>();
       monsterHealthManager = gameObject.GetComponent<MonsterHealthManager>();
       monsterWrapper = gameObject.transform.parent.gameObject;
        defenseComboChecker = monsterWrapper.GetComponent<Key_Validator>();
        //monster key list class
        monsterClass = monster.GetComponent<Monster>();
        //monsterCombos = monsterClass.battleCombos;
        currentCombo = monsterClass.battleCombos[0];
        defenseComboChecker.keyCombo = currentCombo.defense;
       StartCoroutine("InitAttack");

        defensePrompt = GameObject.Find("DefenseCombo").GetComponent<Text>();
        UpdateDefensePrompt();
    }
    void UpdateDefenseCombo()
    {
        currentCombo = monsterClass.currentCombo;
        defenseComboChecker.keyCombo = currentCombo.defense;
        UpdateDefensePrompt();
    }
    void UpdateDefensePrompt()
    {
        monsterIsAlive = monster.GetComponent<MonsterHealthManager>().monsterIsAlive;
        if(!monsterIsAlive)
        {
            return;
        }
        defenseComboText = "defense combo: " + currentCombo.defense[0] + " + " + currentCombo.defense[1];
        defensePrompt.text = defenseComboText;
    }
    IEnumerator InitAttack()
    {
        yield return new WaitForSeconds(1.5f);
        AttackCycle();
    }

    void AttackCycle()
    {
        UpdateDefenseCombo();
        heroCanAttack = false;

        if (healthTracker.playerIsAlive && monsterHealthManager.monsterIsAlive)
        {
            test_attack = Instantiate(ball, attackStartPosition, transform.rotation);
            next_attack_delay = Random.Range(3.0f, 6.0f);
            validDefense = false;
        }
    }

    public IEnumerator OpenDefenseWindow()
    {
        validDefense = true;
        yield return new WaitForSeconds(defense_window);
        validDefense = false;
        StartCoroutine(AssignDamage());
    }
    IEnumerator AssignDamage()
    {
        Destroy(test_attack);

        if (!attackDefended)
        {
            healthTracker.TakeDamage();
        }
        if (healthTracker.playerIsAlive)
        {
            yield return new WaitForSeconds(next_attack_delay);
            heroCanAttack = false;
            AttackCycle();
        }
        attackDefended = false;
        defenseWindowMissed = false;
    }
    void CheckDefense()
    {
        var comboMatch = defenseComboChecker.comboPressed;
        //Debug.Log("combos " + monsterClass.currentCombo.defense[0]);
        if (!validDefense || !comboMatch)
        {
            attackDefended = false;
            defenseWindowMissed = true;
            heroCanAttack = false;
        }
        if(validDefense && comboMatch)
        {
           attackDefended = true;
           heroCanAttack = true;
           hero_action_manager.TriggerDefense();
        }
    }
	void Update () {

        if (Input.anyKeyDown && healthTracker.playerIsAlive && !heroCanAttack)
        {
            CheckDefense();
        }
	}
}
