using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseHelathManager : MonoBehaviour
{
    public int health = 3;
    public bool isDead = false;
    bool deathAnnounced = false;
    public GameEvent creatureDeath;

    // Start is called before the first frame update
    void Start()
    {
        isDead = health > 0;
    }

    public void DecreaseHealth()
    {
        health--;
        this.transform.GetChild(health).gameObject.SetActive(false);
        print($"{this.name} took damage, health is {health}");
    }

    public bool CheckForDeath()
    {
        return health <= 0; 
    }

    // Update is called once per frame
    void Update()
    {
        isDead = health > 0;
        if(isDead && !deathAnnounced)
        {
            creatureDeath.Invoke();
            deathAnnounced = true;
        }
    }
}
