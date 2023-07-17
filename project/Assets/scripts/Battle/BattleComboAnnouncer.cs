using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleComboAnnouncer : MonoBehaviour
{
    BaseMonster baseMonster;
    BattleCombo currentCombo;
    TextMeshProUGUI comboText;
    public void AnnounceCombo()
    {
        UpdateCombo();
        string comboString = $"{currentCombo.keyCode1} + {currentCombo.keyCode2}!";
        comboText.text = comboString;
    }
    void Start()
    {
        baseMonster = FindObjectOfType<BaseMonster>();
        comboText = this.GetComponentInChildren<TextMeshProUGUI>();
        comboText.text = "";
    }

    public void UpdateCombo()
    {
        if (baseMonster.defenseCount < baseMonster.defenseCombos.Count)
        {
            currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
        }
    }
}
