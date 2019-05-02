using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster_action_manager : MonoBehaviour {
    public string current_attack;
    public string defense_key;

    public string[] attack_list;
    public float attack_duration = 2.0f;
    public float defense_window = 0.5f;
    public float defense_start = 1.0f;
    float next_attack_delay;
    public bool validDefense = false;
    bool attackDefended = false;
    bool defenseWindowMissed = false;
    public bool heroCanAttack = false;
    Hero_Health_Value healthTracker;
    GameObject healthIndicator;
    GameObject monster;
    Vector2 attackStartPosition;

    public GameObject ball;
    private GameObject test_attack;

    Hero_Battle_Action_Manager hero_action_manager;

    void Start () {
       monster = GameObject.FindGameObjectWithTag("Enemy");
       attackStartPosition = new Vector2(monster.transform.position.x, monster.transform.position.y + 1.2f);
       hero_action_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero_Battle_Action_Manager>();
       var healthIndicator = GameObject.Find("health_value");
       healthTracker = healthIndicator.GetComponent<Hero_Health_Value>();
       StartCoroutine("InitAttack");
    }
    IEnumerator InitAttack()
    {
        yield return new WaitForSeconds(1.5f);
        AttackCycle();
    }

    void AttackCycle()
    {
        heroCanAttack = false;
        if (healthTracker.playerIsAlive)
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
        if (!validDefense || Input.inputString != defense_key)
        {
            attackDefended = false;
            defenseWindowMissed = true;
            heroCanAttack = false;
        }
        if(validDefense && !defenseWindowMissed)
        {
           attackDefended = true;
           heroCanAttack = true;
           hero_action_manager.TriggerDefense();
        }
    }
	void Update () {
        if(Input.anyKeyDown && healthTracker.playerIsAlive && !heroCanAttack)
        {
            CheckDefense();
        }
	}
}
