using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseFamiliar : BaseCreature
{
    public bool canCounter = false;
    public bool summoned = true;
    bool attackCounterSuccess = false;
    //bool battleChallengeSuccess = false;
    // this should be an event on the Battle UI and not the familiar
    [SerializeField] GameEvent battleChallengeSuccess;
    [SerializeField] GameEvent familiarDeathComplete;
    Animator animator;

    SpriteRenderer[] allSprites;
    Color[] defaultColors;

    // TODO: need array of alphas to account for colors with transparency - see white sprite setter
    void ToggleSprites(bool hideSprites = true)
    {
        for (int i = 0; i < allSprites.Length; i++)
        {
            var newColor = allSprites[i].color;
            newColor.a = 0;
            if (!hideSprites)
            {
                newColor = defaultColors[i];
            }
            allSprites[i].color = newColor;
        }
    }

    public void TriggerAttack()
    {
        //animator?.SetBool(GetTargetAttack(), true);
        //print($"familiar attack trigger index:{defenseCount}");
        //print(GetTargetAttack());
        StartCoroutine(TriggerAttackSequence());
    }

    public void DisableAttack()
    {
        animator?.SetBool(GetTargetAttack(), false);
    }

    IEnumerator TriggerAttackSequence()
    {
        animator?.SetBool(GetTargetAttack(), true);
        yield return new WaitForSeconds(.5f);
        animator?.SetBool(GetTargetAttack(), false);
        IncreaseDefenseCount();
    }

    public void TriggerFamiliarDeathComplete()
    {
        familiarDeathComplete.Invoke();
    }

    public void HideFamiliar()
    {
        ToggleSprites(true);
    }

    public void ShowFamiliar()
    {
        ToggleSprites(false);
        this.GetComponent<Animator>().SetBool("HIDDEN", false);
    }

    public void BringToFront()
    {
        SortingGroup sortingGroup = this.GetComponent<SortingGroup>();
        sortingGroup.sortingLayerID = SortingLayer.NameToID("Creatures");
        sortingGroup.sortingOrder = 100;
    }

    public void SendToBack()
    {
        SortingGroup sortingGroup = this.GetComponent<SortingGroup>();

        sortingGroup.sortingLayerID = SortingLayer.NameToID("Familiar");
        sortingGroup.sortingOrder = 0;
    }

    public void IncreaseDefenseCount()
    {
        defenseCount++;
    }

    void Start()
    {
        allSprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        defaultColors = new Color[allSprites.Length];
        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < allSprites.Length; i++)
        {
            defaultColors[i] = allSprites[i].color;
        }
        if (summoned)
        {
            ShowFamiliar();
        } else
        {
            HideFamiliar();
        }
    }

}
