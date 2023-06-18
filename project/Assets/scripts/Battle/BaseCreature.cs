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
    public bool defenseSuccess = false;
    bool battleChallengeActive = false;
    [HideInInspector] public int attackCount = 0;
    [HideInInspector] public int defenseCount = 0;
    [SerializeField] GameObject hero;
    [SerializeField] GameObject battleUI;
    public List<BattleCombo> defenseCombos;
    public bool canDefend = false;
    [HideInInspector] public int defenseComboIndex = 0;

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
