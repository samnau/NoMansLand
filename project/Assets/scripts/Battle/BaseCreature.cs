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

    public void TriggerDeath()
    {
        //TEMP: revise this for new animated models
        //ColorTweener colorTweener = this.GetComponentInChildren<ColorTweener>();
        isDead = true;
        //SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //sprite.color = Color.red;

        //TEMP: revise this for new animated models
        gameObject.SetActive(false);
        //colorTweener.TriggerAlphaSpriteTween(0);
    }

    public void TriggerVictory()
    {
        victory = true;
        print($"{this.name} wins!");
        //SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //sprite.color = Color.blue;
    }
}
