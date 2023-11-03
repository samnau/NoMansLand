using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleComboAnnouncer : MonoBehaviour
{
    BaseMonster baseMonster;
    BattleCombo currentCombo;
    TextMeshProUGUI comboText;
    [SerializeField] GameObject battleCommandWrapper;
    [SerializeField] GameObject battleCommandArrow;
    Dictionary<KeyCode, float> arrowDirections = new Dictionary<KeyCode, float>();
    public void AnnounceCombo()
    {
        UpdateCombo();
        string comboString = $"{currentCombo.keyCode1} + {currentCombo.keyCode2}!";
        if(comboText)
        {
            comboText.text = comboString;
        }
    }

    IEnumerator ShowBattleCommand()
    {
        float targetRotation;

        if (arrowDirections.TryGetValue(currentCombo.keyCode1, out targetRotation))
        {
            battleCommandArrow.transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        }
        else
        {
            battleCommandArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        yield return new WaitForSeconds(2f);
        ToggleBattleCommand();
    }

    void ToggleBattleCommand()
    {
        if(battleCommandWrapper)
        {
            SpriteRenderer[] commandRenderers = battleCommandWrapper.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in commandRenderers)
            {
                sprite.enabled = !sprite.enabled;
            }
        }
    }
    void Start()
    {
        arrowDirections.Add(KeyCode.LeftArrow, 270f);
        arrowDirections.Add(KeyCode.RightArrow, 90f);
        arrowDirections.Add(KeyCode.UpArrow, 180f);
        arrowDirections.Add(KeyCode.DownArrow, 0f);

        baseMonster = FindObjectOfType<BaseMonster>();
        comboText = this.GetComponentInChildren<TextMeshProUGUI>();
        if(comboText)
        {
            comboText.text = "";
        }

        if (baseMonster.defenseCount < baseMonster.defenseCombos.Count)
        {
            currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
        }
        ToggleBattleCommand();
        StartCoroutine(ShowBattleCommand());
    }

    public void UpdateCombo()
    {
        if (baseMonster.defenseCount < baseMonster.defenseCombos.Count)
        {
            currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
        }
    }
}
