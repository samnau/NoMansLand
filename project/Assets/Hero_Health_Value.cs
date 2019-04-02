using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_Health_Value : MonoBehaviour {
    int defaultHealth = 4;
    public int healthValue = 0;
    public bool playerIsAlive = true;
    Text textbox;
    Camera mainCamera;
    GameObject player;

	void Start () {
        mainCamera = Camera.main;
        healthValue = defaultHealth;
        textbox = gameObject.GetComponent<Text>();
        textbox.text = healthValue.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerMotionController>().inBattle = true;

	}
	public void TakeDamage()
    {
        if(healthValue > 0)
        {
            healthValue--;
            StartCoroutine(IndicateDamage());
        }
    }
    void UpdateHealthValue(bool increase = true)
    {
        textbox.text = healthValue.ToString();
        if(healthValue < 1)
        {
            textbox.text = healthValue.ToString() + " you died";
            playerIsAlive = false;
            //IndicateDeath();
            //mainCamera.GetComponent<Camera_Shaker>().enabled = true;
        }
    }
    void IndicateDeath()
    {
        
        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
        {
            Debug.Log(sr);
            var deadColor = new Color(0, 0, 0, 0.5f);
            sr.material.color = Color.Lerp(Color.white, deadColor, 1.5f);
        }
    }
    IEnumerator IndicateDamage()
    {
        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
        {
            Debug.Log(sr);
            sr.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
        {
            Debug.Log(sr);
            sr.material.color = Color.white;
        }
    }
    void Update()
    {
        UpdateHealthValue();
    }
}
