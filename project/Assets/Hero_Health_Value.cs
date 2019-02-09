using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_Health_Value : MonoBehaviour {
    int healthValue = 0;
    Text textbox;
	// Use this for initialization
	void Start () {
        textbox = gameObject.GetComponent<Text>();
        textbox.text = healthValue.ToString();
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotionController>().enabled = false;
       // Debug.Log(textbox);
        //textbox.GetComponent<Text>().text = healthValue;
	}
	
    void UpdateHealthValue(bool increase = true)
    {
        if (increase)
        {
            healthValue++;
        } else
        {
            healthValue--;
        }
        textbox.text = healthValue.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown("down"))
        {
            UpdateHealthValue(false);
        }
        if (Input.GetKeyDown("up"))
        {
            UpdateHealthValue();
        }

    }
}
