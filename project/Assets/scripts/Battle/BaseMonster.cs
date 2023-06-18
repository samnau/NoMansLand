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
    [SerializeField] GameEvent takeDamage;
    [SerializeField] GameEvent dealDamage;
    [SerializeField] GameEvent startAttack;
    [SerializeField] GameEvent startDefense;

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
        canDefend = false;
    }

    void CheckCombo(List<BattleCombo> targetComboList, ref int targetComboIndex)
    {
        if(targetComboIndex >= targetComboList.Count)
        {
            return;
        }
        List<KeyCode> comboList = new List<KeyCode> { targetComboList[targetComboIndex].keyCode1, targetComboList[targetComboIndex].keyCode2 };

        // TEMP: logging variable for the key pressed
        KeyCode matchedKey = KeyCode.None;
        foreach (KeyCode keyCode in comboList)
        {
            if (Input.GetKeyDown(keyCode))
            {
                matchedKey = keyCode;
                if (!pressedKeys.Contains(keyCode))
                {
                    pressedKeys.Add(keyCode);
                    comboMatchCount++;
                }
                print($"target key match:{matchedKey}, match count: {comboMatchCount}");
            }
        }
        if (comboMatchCount > 0)
        {
            StartCoroutine(ComboIntervalReset());
        }
        if (comboMatchCount >= comboList.Count)
        {
            comboMatchCount = 0;
            targetComboIndex++;
            canDefend = false;
            defenseSuccess = true;
            print("combo success");
            takeDamage.Invoke();
        }
        if (targetComboIndex < targetComboList.Count)
        {
            print($"target key combo match count {defenseCount}");
            print($"arrow key: {targetComboList[targetComboIndex].keyCode1}");
        }

    }

    IEnumerator MoveToFamiliar()
    {
        Vector3 originalPosition = transform.position;
        PositionTweener positionTweener = gameObject.GetComponent<PositionTweener>();
        Vector3 targetPosition = familiar.transform.position;
        positionTweener.TriggerPosition(targetPosition, 12f);
        yield return new WaitForSeconds(2.5f);
        positionTweener.TriggerPosition(originalPosition, 18f);
        if(!defenseSuccess)
        {
            dealDamage.Invoke();
        }
    }

    void CheckDefenseCombo()
    {
        List<KeyCode> comboList = new List<KeyCode> { defenseCombos[defenseCount].keyCode1, defenseCombos[defenseCount].keyCode2 };
        
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
            //print($"defense key combo match");
            defenseSuccess = true;
            comboMatchCount = 0;
            //defenseCount++;
            counterComboIndex++;
        }
        print($"defense key combo match count {counterComboIndex}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isFamiliar = collision.name == familiar.name;
        if(isFamiliar)
        {
            print($"{collision.name} trigger");
            canDefend = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isFamiliar = collision.name == familiar.name;
        if (isFamiliar)
        {
            print($"{collision.name} trigger exit");
            canDefend = false;
            print("attack over");
        }
    }

    public void StartAttack()
    {
        print("Rarr! I am starting my attack");
        startAttack.Invoke();
        StartCoroutine(MoveToFamiliar());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && canDefend)
        {
            //CheckDefenseCombo();
            CheckCombo(defenseCombos, ref defenseCount);
            //print($"defesne key press:{CheckDefenseCombo()}");
        }
        
    }
}
