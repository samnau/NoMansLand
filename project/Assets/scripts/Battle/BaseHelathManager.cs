using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseHelathManager : MonoBehaviour
{
    public int health = 3;
    public bool isDead = false;
    bool deathAnnounced = false;
    public GameEvent creatureDeath;
    GameObject[] healthUI;

    void Start()
    {
        isDead = health > 0;
        healthUI = GameObject.FindGameObjectsWithTag("health");
        InitHideHealthUI();
        //HideHealthUI();
    }

    public void DecreaseHealth()
    {
        if(health > 0)
        {
            health--;
            this.transform.GetChild(health).gameObject.SetActive(false);
        }
    }

    public bool CheckForDeath()
    {
        return health <= 0; 
    }

    public void InitHideHealthUI()
    {
        foreach (GameObject healthUIItem in healthUI)
        {
            Image[] childArray = healthUIItem.GetComponentsInChildren<Image>();
            foreach (Image childItem in childArray)
            {
                childItem.enabled = false;
            }
        }
    }


    public void HideHealthUI ()
    {
        foreach(GameObject healthUIItem in healthUI)
        {
            healthUIItem.SetActive(false);
        }
    }

    public void ShowHealthUI()
    {
        foreach (GameObject healthUIItem in healthUI)
        {
            Image[] childArray = healthUIItem.GetComponentsInChildren<Image>();
            foreach (Image childItem in childArray)
            {
                childItem.enabled = true;
            }
        }
    }

    // TODO: maybe convert to event?
    void Update()
    {
        isDead = health <= 0;
        if(isDead && !deathAnnounced)
        {
            creatureDeath.Invoke();
            deathAnnounced = true;
        }
    }
}
