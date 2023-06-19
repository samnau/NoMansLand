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
    public bool canDefend = false;
    [HideInInspector] public int defenseComboIndex = 0;

    // having these in the base is questionable
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print($"creature {gameObject.name} is being hit");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print($"creature {gameObject.name} is done being hit");
    }

}
