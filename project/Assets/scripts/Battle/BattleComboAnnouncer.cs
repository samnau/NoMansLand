using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class BattleComboAnnouncer : MonoBehaviour
{
    BaseMonster baseMonster;
    BattleCombo currentCombo;
    TextMeshProUGUI comboText;
    [SerializeField] GameObject battleCommandWrapper;
    [SerializeField] GameObject battleCommandArrow;
    Dictionary<KeyCode, float> arrowDirections = new Dictionary<KeyCode, float>();
    float tweenSpeed = 6f;
    GameEvent showCommand;

    public void AnnounceCombo()
    {
        UpdateCombo();
        StartCoroutine(ShowBattleCommand());
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
        ToggleBattleCommand();
        yield return new WaitForSeconds(1.5f);
        ToggleBattleCommand();
    }

    void RevealBattleCommand()
    {
        ColorTweener[] colorTweeners = this.GetComponentsInChildren<ColorTweener>();
        foreach (ColorTweener tweener in colorTweeners)
        {
            tweener.TriggerAlphaSpriteTween(1f, tweenSpeed);
        }
    }

    void HideBattleCommand(bool instant = false)
    {
        ColorTweener[] colorTweeners = this.GetComponentsInChildren<ColorTweener>();

        foreach (ColorTweener tweener in colorTweeners)
        {
            if(instant)
            {
                tweener.SetSpriteAlpha();
            } else
            {
                tweener.TriggerAlphaSpriteTween(0, tweenSpeed);
            }
        }
    }


    void ToggleBattleCommand()
    {
        print("ToggleCpommand");
        if(battleCommandWrapper)
        {
            SpriteRenderer[] commandRenderers = battleCommandWrapper.GetComponentsInChildren<SpriteRenderer>();

            float alphaValue = commandRenderers[0].color.a;

            if(alphaValue == 0)
            {
                RevealBattleCommand();
            } else
            {
                HideBattleCommand();
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
        if(baseMonster == null)
        {
            return;
        }
        if(comboText)
        {
            comboText.text = "";
        }

        if (baseMonster.defenseCount < baseMonster.defenseCombos.Count)
        {
            currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
        }
        HideBattleCommand(true);
    }

    public void UpdateCombo()
    {
        if (baseMonster.defenseCount < baseMonster.defenseCombos.Count)
        {
            currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
        }
    }
}
