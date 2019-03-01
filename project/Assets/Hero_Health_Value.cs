using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_Health_Value : MonoBehaviour {
    int defaultHealth = 4;
    public int healthValue = 0;
    Text textbox;
    Camera mainCamera;

	void Start () {
        mainCamera = Camera.main;
        healthValue = defaultHealth;
        textbox = gameObject.GetComponent<Text>();
        textbox.text = healthValue.ToString();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotionController>().inBattle = true;
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
        textbox.text = healthValue.ToString();
        if(healthValue < 1)
        {
            textbox.text = healthValue.ToString() + " you died";
            mainCamera.GetComponent<Camera_Shaker>().enabled = true;
        }
    }
    void Update()
    {
        UpdateHealthValue();
    }
}
