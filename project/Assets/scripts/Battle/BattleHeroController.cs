using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHeroController : MonoBehaviour
{
    BaseMonster baseMonster;
    BattleCombo currentCombo;
    public void AnnounceCombo()
    {
        //print("Defend with the combo!");
        UpdateCombo();
        print($"Molly yells out: {currentCombo.keyCode1} + {currentCombo.keyCode2}");
    }
    // Start is called before the first frame update
    void Start()
    {
        baseMonster = FindObjectOfType<BaseMonster>();
        //UpdateCombo();
    }

    public void UpdateCombo()
    {
        currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
    }

    public void StartCounter()
    {
        currentCombo = baseMonster.counterCombos[baseMonster.counterCount];
        AnnounceCombo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
