using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : BaseCreature
{
    bool hasRequiredFamiliar = false;
    bool canCounter = false;
    [HideInInspector] public int counterCount = 0;
    public List<BattleCombo> counterCombos;
    List<KeyCode> pressedKeys = new List<KeyCode>();
    int counterComboIndex = 0;
    float comboInterval = .125f;
    int currentComboMatchCount = 0;
    [SerializeField] GameEvent takeDamage;
    [SerializeField] GameEvent dealDamage;
    [SerializeField] GameEvent startAttack;
    [SerializeField] GameEvent defenseSuccess;
    [SerializeField] GameEvent battleChallengeStart;
    [SerializeField] GameEvent counterStart;
    [SerializeField] GameEvent counterSuccess;

    public enum Familiars
    { frog, bear, raven, dragon }

    public Familiars requiredFamiliar;

    public enum Runes
    { power, speed, magic, harmony }

    public Runes requiredRune;

    bool hasRequiredRune = false;
    [SerializeField] GameObject familiar;

    // resets combo key presses after a short interval and disables the ability to defend
    // TODO: should disable ability to counter also 
    IEnumerator ComboIntervalReset()
    {
        yield return new WaitForSeconds(comboInterval);
        currentComboMatchCount = 0;
        pressedKeys = new List<KeyCode>();
        canDefend = false;
    }

    // checks the passed list of combos with an index for targeting the current combo to check
    void CheckCombo(List<BattleCombo> targetComboList, ref int targetComboIndex)
    {
        if(targetComboIndex >= targetComboList.Count)
        {
            return;
        }
        List<KeyCode> comboList = new List<KeyCode> { targetComboList[targetComboIndex].keyCode1, targetComboList[targetComboIndex].keyCode2 };

        // TEMP: logging variable for the key pressed
        KeyCode matchedKey = KeyCode.None;

        // runs through all the keys in the combo and checks them against current user input
        // matches get added to a temp list that gets reset after interval
        foreach (KeyCode keyCode in comboList)
        {
            if (Input.GetKeyDown(keyCode))
            {
                matchedKey = keyCode;
                if (!pressedKeys.Contains(keyCode))
                {
                    pressedKeys.Add(keyCode);
                    currentComboMatchCount++;
                }
                print($"target key match:{matchedKey}, match count: {currentComboMatchCount}");
            }
        }

        // starts a routine to clear out key matches after interval
        if (currentComboMatchCount > 0)
        {
            StartCoroutine(ComboIntervalReset());
        }
        if (currentComboMatchCount >= comboList.Count)
        {
            targetComboIndex++;
            // defense / counter logic

            if (canDefend)
            {
                defenseSuccess.Invoke();
                battleChallengeStart.Invoke();
                canCounter = true;
            }

        }

        // this is currently just test code 
        if (targetComboIndex < targetComboList.Count)
        {
            print($"target key combo match count {defenseCount}");
            //print($"arrow key: {targetComboList[targetComboIndex].keyCode1}");
        }

    }

    // TEMP: to test basic collistion functionality. might become a real method with new name
    IEnumerator MoveToFamiliar()
    {
        Vector3 originalPosition = transform.position;
        PositionTweener positionTweener = gameObject.GetComponent<PositionTweener>();
        Vector3 targetPosition = familiar.transform.position;
        positionTweener.TriggerPosition(targetPosition, 18f);
        yield return new WaitForSeconds(1.0f);
        positionTweener.TriggerPosition(originalPosition, 18f);
    }


    // use collision enter to enable the ability to defend
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isFamiliar = collision.name == familiar.name;
        if(isFamiliar)
        {
            print($"{collision.name} trigger");
            canDefend = true;
        }
    }

    // use collision exit to disable the ability to defend
    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isFamiliar = collision.name == familiar.name;
        if (isFamiliar)
        {
            print($"{collision.name} trigger exit");
            canDefend = false;
            //print("attack over");
            if(!canCounter)
            {
                //counterStart.Invoke();
                dealDamage.Invoke();
                //takeDamage.Invoke();
            } else
            {
                //dealDamage.Invoke();
            }
        }
    }

    public void StartAttack()
    {
        print("Rarr! I am starting my attack");
        startAttack.Invoke();
        StartCoroutine(MoveToFamiliar());
    }

    // check in update for input only if defense is enabled
    //TODO: needs logic to check for defense or counter combos
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(canDefend)
            {
                CheckCombo(defenseCombos, ref defenseCount);
            }

            // expand on this later if the current flow doesn't turn out well
            //if(canCounter)
            //{
            //    CheckCombo(counterCombos, ref counterCount);
            //}
        }
        
    }
}
