using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : BaseCreature
{
    bool hasRequiredFamiliar = false;
    public List<BattleCombo> counterCombos;
    List<KeyCode> pressedKeys = new List<KeyCode>();
    int counterComboIndex = 0;
    float comboInterval = 2f;
    int comboMatchCount = 0;

    public enum Familiars
    { frog, bear, raven, dragon }

    public Familiars requiredFamiliar;

    public enum Runes
    { power, speed, magic, harmony }

    public Runes requiredRune;

    bool hasRequiredRune = false;
    [SerializeField] GameObject familiar;

    IEnumerator ComboIntervalReset()
    {
        yield return new WaitForSeconds(.125f);
        comboMatchCount = 0;
        pressedKeys = new List<KeyCode>();
    }

    void CheckDefenseCombo()
    {
        List<KeyCode> comboList = new List<KeyCode> { defenseCombos[defenseComboIndex].keyCode1, defenseCombos[defenseComboIndex].keyCode2 };
        
        // TEMP: logging variable for the key pressed
        KeyCode matchedKey = KeyCode.None;
        foreach(KeyCode keyCode in comboList)
        {
            if(Input.GetKeyDown(keyCode))
            {
                matchedKey = keyCode;
                if(!pressedKeys.Contains(keyCode))
                {
                    pressedKeys.Add(keyCode);
                    comboMatchCount++;
                }
                print($"defense key match:{matchedKey}, match count: {comboMatchCount}");
            }
        }
        if (comboMatchCount > 0)
        {
            StartCoroutine(ComboIntervalReset());
        }
        if (comboMatchCount >= comboList.Count)
        {
            print($"defense key combo match");
            comboMatchCount = 0;
            //defenseComboIndex++;
            counterComboIndex++;
        }
        print($"defense key combo match count {counterComboIndex}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            CheckDefenseCombo();
            //print($"defesne key press:{CheckDefenseCombo()}");
        }
        
    }
}
