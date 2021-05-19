using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthManager : MonoBehaviour {
    public int healthValue = 3;
    Animator monsterAnimator;
    //Text textbox;
    public bool monsterIsAlive = true;
    bool winState = false;

	// Use this for initialization
	void Start () {
        //  textbox = gameObject.GetComponent<Text>();
        //textbox.text = healthValue.ToString();
        monsterAnimator = gameObject.GetComponent<Animator>();
    }
	public void TakeDamage()
    {
        if(healthValue > 0)
        {
            healthValue -= 1;
           // textbox.text = healthValue.ToString();
        }
    }

    void ShowWinState ()
    {
        if (!winState)
        {
            GameObject.Find("fpo_shadow").SetActive(false);
            GameObject.Find("AttackCombo").SetActive(false);
            GameObject.Find("DefenseCombo").GetComponent<Text>().text = "You Win!";
            winState = true;
        }
    }
	// Update is called once per frame
	void Update () {
        monsterIsAlive = healthValue > 0;
        if(!monsterIsAlive)
        {
            monsterAnimator.SetBool("defeat", true);
            ShowWinState();
            //textbox.text = "you win!!";
        }
    }
}
