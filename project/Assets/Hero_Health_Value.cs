using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_Health_Value : MonoBehaviour {
    int defaultHealth = 4;
    public int healthValue = 0;
    Text textbox;
	// Use this for initialization
	void Start () {
        healthValue = defaultHealth;
        textbox = gameObject.GetComponent<Text>();
        textbox.text = healthValue.ToString();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotionController>().inBattle = true;
       // Debug.Log(textbox);
        //textbox.GetComponent<Text>().text = healthValue;
	}
	public void TakeDamage()
    {
        if(healthValue > 0)
        {
            healthValue--;
        }
    }
    void UpdateHealthValue(bool increase = true)
    {
       // if (increase && healthValue < defaultHealth)
       // {
       //     healthValue++;
       //} else if(healthValue > 0)
       // {
       //     healthValue--;
       // }
        textbox.text = healthValue.ToString();
    }
    void Update()
    {
        //if (Input.GetKeyDown("down"))
        //{
        //    UpdateHealthValue(false);
        //}
        //if (Input.GetKeyDown("up"))
        //{
        //    UpdateHealthValue();
        //}
        UpdateHealthValue();
    }
}
