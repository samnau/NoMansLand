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
    bool validDefense = false;
    bool attackDefended = false;
    bool defenseWindowMissed = false;
    //bool attackComplete = false;
    Hero_Health_Value healthTracker;
    Text textbox;
    GameObject healthIndicator;

    public GameObject ball;
    private GameObject test_attack;

    Hero_Battle_Action_Manager hero_action_manager;

    void Start () {
       hero_action_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero_Battle_Action_Manager>();
       textbox = gameObject.GetComponent<Text>();
       var healthIndicator = GameObject.Find("health_value");
       healthTracker = healthIndicator.GetComponent<Hero_Health_Value>();
        StartCoroutine("InitAttack");
    }
    IEnumerator InitAttack()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("AttackCycle");
    }

    IEnumerator AttackCycle()
    {
        if (healthTracker.playerIsAlive)
        {
            test_attack = Instantiate(ball, transform.position, transform.rotation);
            next_attack_delay = Random.Range(3.0f, 6.0f);
            //attackComplete = false;
            validDefense = false;
            //textbox.text = "attacking!";
            yield return new WaitForSeconds(defense_start);
            validDefense = true;
            //textbox.text = "defend!";
            if(defenseWindowMissed)
            {
                StartCoroutine(AssignDamage());
                StopCoroutine("AttackCycle");
            }

            yield return new WaitForSeconds(defense_window);

            //textbox.text = "attacking complete!";

            //attackComplete = true;
            validDefense = false;
            
            StartCoroutine(AssignDamage());
        }
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
            StartCoroutine("AttackCycle");
        }
        attackDefended = false;
        defenseWindowMissed = false;
    }
    void CheckDefense()
    {
       if(!validDefense || Input.inputString != defense_key)
        {
            //textbox.text = "defend failed!";
            attackDefended = false;
            defenseWindowMissed = true;
        }
        if(validDefense && !defenseWindowMissed)
        {
           attackDefended = true;
            hero_action_manager.TriggerDefense();
           //textbox.text = "blocked";
        }
    }
	void Update () {
        if(Input.anyKeyDown && healthTracker.playerIsAlive)
        {
            CheckDefense();
        }
	}
}
