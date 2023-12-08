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
    [SerializeField] GameEvent shakeCamera;

    // NOTE: for demo only, remove later
    [SerializeField] GameObject creatureShadow;

    public void TriggerCameraShake()
    {
        shakeCamera.Invoke();
    }
    public void TriggerDeath()
    {
        //TEMP: revise this for new animated models
        //ColorTweener colorTweener = this.GetComponentInChildren<ColorTweener>();
        isDead = true;
        //SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //sprite.color = Color.red;

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.clear;
        }
        // NOTE: for demo only, remove later
        creatureShadow.SetActive(false);
        //TEMP: revise this for new animated models
        //gameObject.SetActive(false);
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
