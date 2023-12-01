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
    [SerializeField] GameEvent announceCombo;
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
        print($"defense count:{targetComboIndex}");

        if (targetComboIndex >= targetComboList.Count)
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

    }

    // TEMP: to test basic collistion functionality. might become a real method with new name
    IEnumerator MoveToFamiliar()
    {
        Vector3 originalPosition = transform.position;
        PositionTweener positionTweener = gameObject.GetComponent<PositionTweener>();
        Vector3 targetPosition = familiar.transform.position;
        Vector3 currentPos = this.transform.position;
        var upValue = currentPos.y + 10f;
        Vector3 upPosition = new Vector3(currentPos.x, upValue, currentPos.z);
        positionTweener.TriggerPosition(upPosition, 18f);
        yield return new WaitForSeconds(.5f);
        Vector3 overPosition = new Vector3(targetPosition.x, upValue, currentPos.z);
        positionTweener.TriggerPosition(overPosition, 18f);
        yield return new WaitForSeconds(.5f);

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
            canDefend = true;
        }
    }

    // use collision exit to disable the ability to defend
    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isFamiliar = collision.name == familiar.name;
        if (isFamiliar)
        {
            canDefend = false;
            if(!canCounter)
            {
                //counterStart.Invoke();
                dealDamage.Invoke();
                StartNextAttackCycle();
            }
        }
    }

    public void ShowDamage()
    {
        StartCoroutine(DamageShake());
    }

    IEnumerator DamageShake()
    {
        float shakeDelay = .05f;
        float flucation = .5f;
        Quaternion startRotation = transform.rotation;
        Quaternion damageRotation = Quaternion.Euler(0, 0, -15f);
        Vector3 origin = transform.position;
        Vector3 leftPos = new Vector3(origin.x + flucation, origin.y, origin.z);
        Vector3 rightPos = new Vector3(origin.x - flucation, origin.y, origin.z);

        SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;

        transform.rotation = damageRotation;

        transform.position = leftPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = rightPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = leftPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = rightPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = origin;
        transform.rotation = startRotation;
        sprite.color = Color.white;

    }

    public void TriggerAnnounceCombo()
    {
        announceCombo.Invoke();
        canDefend = true;
    }

    public void TriggerDamage()
    {
        canDefend = false;
        if (!canCounter)
        {
            //counterStart.Invoke();
            dealDamage.Invoke();

            // TEMP: disable until ready to test
            StartNextAttackCycle();
        }
    }

    public void StartAttack()
    {
        if(!isDead && !victory)
        {
            startAttack.Invoke();
            print("MONSTER ATTACKING!");
            //StartCoroutine(MoveToFamiliar());
        }
    }

    public void StartNextAttackCycle ()
    {
        canCounter = false;
        StartCoroutine(StartNextAttack());
    }

    IEnumerator StartNextAttack()
    {
        gameObject.GetComponent<Animator>()?.SetBool("ATTACK1", false);
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>()?.SetBool("ATTACK1", true);
        StartAttack();
    }

    // keep the target defense combo the same if rune ring challenge fails
    public void ReduceDefenseCount()
    {
        if(defenseCount > 0)
        {
            defenseCount--;
        }
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
