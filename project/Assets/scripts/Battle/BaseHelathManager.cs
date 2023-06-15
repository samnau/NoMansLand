using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseHelathManager : MonoBehaviour
{
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DecreaseHealth()
    {
        health--;
        print($"{this.name} took damage, health is {health}");
    }

    public bool CheckForDeath()
    {
        return health <= 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
