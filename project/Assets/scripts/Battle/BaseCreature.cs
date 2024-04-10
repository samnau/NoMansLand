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
    protected List<string> attackTriggers = new List<string> { "ATTACK1", "ATTACK2", "ATTACK3" };

    [SerializeField] protected GameEvent completeAttack;
    [SerializeField] protected GameEvent dealDamage;


    // NOTE: for demo only, remove later
    [SerializeField] GameObject creatureShadow;

    // Methods to visually show the damage state with animation triggers
    public void ShowDamage()
    {
        this.GetComponent<Animator>().SetBool("DAMAGE", true);
        StartCoroutine(HideDamage());
    }

    IEnumerator HideDamage()
    {
        yield return new WaitForSeconds(.35f);
        this.GetComponent<Animator>().SetBool("DAMAGE", false);
    }
    public void TriggerCameraShake()
    {
        shakeCamera.Invoke();
    }

    protected string GetTargetAttack()
    {
        int targetIndex = defenseCount <= attackTriggers.Count - 1 ? defenseCount : attackTriggers.Count - 1;
        // NOTE: revsit string template attack targeting later
        //return $"ATTACK{defenseCount+1}";
        return attackTriggers[targetIndex];
    }

    public void ResetAnimationState()
    {
        //this.GetComponent<Animator>().SetBool("RESET", true);
        if(this.GetComponent<Animator>().GetBool("RESET"))
        {
            StartCoroutine(DeactivateReset());
        }
        //StartCoroutine(DeactivateReset());
    }

    IEnumerator DeactivateReset()
    {
        yield return new WaitForSeconds(.35f);
        this.GetComponent<Animator>().SetBool("RESET", false);
    }

    public void DealDamage()
    {
        dealDamage.Invoke();
    }
    public void TriggerDeath()
    {
        //TEMP: revise this for new animated models
        //ColorTweener colorTweener = this.GetComponentInChildren<ColorTweener>();
        isDead = true;
        Animator animator = this.GetComponent<Animator>();
        animator?.SetBool("DEATH", true);
        //SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //sprite.color = Color.red;

        //SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        //foreach(SpriteRenderer sprite in sprites)
        //{
        //    sprite.color = Color.clear;
        //}
        // NOTE: for demo only, remove later
        //creatureShadow.SetActive(false);

    }

    public void HideCreature ()
    {
        SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
    }

    public void TriggerVictory()
    {
        victory = true;
        print($"{this.name} wins!");
        //SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //sprite.color = Color.blue;
    }

    public void TriggerCompleteAttack()
    {
        completeAttack.Invoke();
    }
}
