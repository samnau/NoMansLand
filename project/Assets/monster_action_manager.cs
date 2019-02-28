using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster_action_manager : MonoBehaviour {
   // public string[] attack_list = new string[3];
    string current_attack;
    public string[] attack_list = { "bite", "drain", "slime" };
    bool validDefense = false;
    bool attackDefended = false;
    bool attackComplete = false;
    Text textbox;
    GameObject healthIndicator;
    Component healthTracker;

	void Start () {
        textbox = gameObject.GetComponent<Text>();
        var healthIndicator = GameObject.Find("health_value");
       healthTracker = healthIndicator.GetComponent<Hero_Health_Value>();
        
        StartCoroutine(AttackCycle());
    }
    IEnumerator AttackCycle()
    {
        attackComplete = false;
        validDefense = false;
        textbox.text = "attacking!";
        yield return new WaitForSeconds(2.0f);
        validDefense = true;
        textbox.text = "defend!";
        yield return new WaitForSeconds(1.0f);
        textbox.text = "attacking complete!";

        attackComplete = true;
        validDefense = false;
        StartCoroutine(AssignDamage());
    }

    IEnumerator AssignDamage()
    {
        var fatalValue = 1;
        var heroHealth = healthTracker.GetComponent<Hero_Health_Value>();
        var healthAmount = heroHealth.healthValue;
        if (!attackDefended)
        {
            heroHealth.TakeDamage();
        }
        if (healthAmount > 0)
        {
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(AttackCycle());
        } else if(healthAmount < fatalValue)
        {
            textbox.text = "You died!";
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
