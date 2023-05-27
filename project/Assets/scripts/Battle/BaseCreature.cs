using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    // array of attacks goes here
    // array of counters goes here
    // array of defenses goes here
    public bool isDead = false;
    bool isDamaged = false;
    bool attackSuccess = false;
    bool defenseSuccess = false;
    public int health = 3;
    [SerializeField] GameObject hero;
    [SerializeField] GameObject battleUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print($"creature {gameObject.name} is being hit");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print($"creature {gameObject.name} is done being hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
