using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster_action_manager : MonoBehaviour {
    public string current_attack;
    public string defense_key;

    public float attack_duration = 2.0f;
    public float defense_window = 0.5f;
    public float defense_start = 1.0f;
    float next_attack_delay;
    bool validDefense = false;
    bool attackDefended = false;
    //bool attackComplete = false;
    Hero_Health_Value healthTracker;
    int heroHealthAmount;
    Text textbox;
    GameObject healthIndicator;

	void Start () {
       textbox = gameObject.GetComponent<Text>();
       var healthIndicator = GameObject.Find("health_value");
       healthTracker = healthIndicator.GetComponent<Hero_Health_Value>();
       heroHealthAmount = healthTracker.healthValue;
       StartCoroutine(AttackCycle());
       
    }
    IEnumerator AttackCycle()
    {
        if (heroHealthAmount > 0)
        {

            //attackComplete = false;
            validDefense = false;
            textbox.text = "attacking!";
            yield return new WaitForSeconds(defense_start);
            validDefense = true;
            textbox.text = "defend!";
            yield return new WaitForSeconds(defense_window);
            textbox.text = "attacking complete!";

            //attackComplete = true;
            validDefense = false;
            next_attack_delay = Random.Range(3.0f, 6.0f);
            StartCoroutine(AssignDamage());
        }
    }

    IEnumerator AssignDamage()
    {
        heroHealthAmount = healthTracker.healthValue;

        if (!attackDefended)
        {
            healthTracker.TakeDamage();
        }
        if (heroHealthAmount > 0)
        {
            yield return new WaitForSeconds(next_attack_delay);
            StartCoroutine(AttackCycle());
        }
        attackDefended = false;

    }
    void CheckDefense()
    {
       if(!validDefense)
        {
            textbox.text = "defend failed!";
            attackDefended = false;
        }
        if(validDefense)
        {
           attackDefended = true;
           textbox.text = "blocked";
        }
    }
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckDefense();
        }
	}
}
