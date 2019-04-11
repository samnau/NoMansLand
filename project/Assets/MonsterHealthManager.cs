using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthManager : MonoBehaviour {
    public int healthValue = 3;
    Text textbox;
	// Use this for initialization
	void Start () {
        textbox = gameObject.GetComponent<Text>();
        textbox.text = healthValue.ToString();
    }
	public void TakeDamage()
    {
        if(healthValue > 0)
        {
            healthValue -= 1;
        }
        textbox.text = healthValue.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
