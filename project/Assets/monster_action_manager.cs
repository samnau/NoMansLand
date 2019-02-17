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

	// Use this for initialization
	void Start () {
        textbox = gameObject.GetComponent<Text>();
        var healthIndicator = GameObject.Find("health_value");
       healthTracker = healthIndicator.GetComponent<Hero_Health_Value>();
        
        StartCoroutine(StartAttack());

        //current_attack = attack_list[0];

        //textbox.text = current_attack;
    }
	//IEnumerator StartAttack()
 //   {
 //       yield return new WaitForSeconds(3.0f);
 //       CheckDefense();
 //       //startCoroutine(CheckDefense())
 //   }
    IEnumerator StartAttack()
    {
        attackComplete = false;
        textbox.text = "attacking!";
        yield return new WaitForSeconds(2.0f);
        validDefense = true;
        yield return new WaitForSeconds(1.0f);
        validDefense = false;
        attackComplete = true;
        CheckDefense();
    }
    void CheckDefense()
    {
       // Debug.Log(healthIndicator.GetComponent<Hero_Health_Value>().healthValue);
        if(validDefense)
        {
           attackDefended = true;
           textbox.text = "blocked";
           Debug.Log("blocked!");
        }else if(!validDefense && attackComplete)
        {
            textbox.text = "damage!";
            StartCoroutine(StartAttack());
            healthTracker.GetComponent<Hero_Health_Value>().TakeDamage();
            if(healthTracker.GetComponent<Hero_Health_Value>().healthValue == 0)
            {
                textbox.text = "you died!";
            }
        }
    }
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.D))
        {
            //current_attack = attack_list[1];
            CheckDefense();
            //textbox.text = attackDefended.ToString();
        }
        //if(Input.GetKeyUp(KeyCode.D))
        //{
        //    StartAttack();
        //}
	}
}
