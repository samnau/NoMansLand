using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    bool battleChallengeActive = false;
    [HideInInspector] public int attackCount = 0;
    [HideInInspector] public int defenseCount = 0;
    [SerializeField] GameObject hero;
    [SerializeField] GameObject battleUI;
    public List<BattleCombo> defenseCombos;
    [HideInInspector] public bool canDefend = false;
    [HideInInspector] public int defenseComboIndex = 0;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool victory = false;

    //// having these in the base is questionable
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print($"creature {gameObject.name} is being hit");
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    print($"creature {gameObject.name} is done being hit");
    //}
    public void TriggerDeath()
    {
        isDead = true;
        SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;
    }

    public void TriggerVictory()
    {
        victory = true;
        print($"{this.name} wins!");
        SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.blue;
    }
}
