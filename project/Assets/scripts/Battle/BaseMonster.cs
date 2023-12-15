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
    //List<string> attackTriggers = new List<string> { "ATTACK1", "ATTACK1", "ATTACK1" };
    int counterComboIndex = 0;
    float comboInterval = .125f;
    int currentComboMatchCount = 0;

    Animator animator;

    [SerializeField] GameEvent announceCombo;
    [SerializeField] GameEvent takeDamage;
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

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void FreezeAnimation()
    {
        animator.speed = 0;
    }

    public void RewindAnimation()
    {
        animator.speed = -.75f;
    }

    public void UnFreezeAnimation()
    {
        animator.speed = 1;
    }

    // resets combo key presses after a short interval and disables the ability to defend
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
        if (targetComboIndex >= targetComboList.Count)
        {
            return;
        }
        List<KeyCode> comboList = new List<KeyCode> { targetComboList[targetComboIndex].keyCode1, targetComboList[targetComboIndex].keyCode2 };

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

            animator?.SetBool(GetTargetAttack(), false);
            if (canDefend)
            {
                defenseSuccess.Invoke();
                battleChallengeStart.Invoke();
                canCounter = true;
            }

        }

    }

    // TEMP: to test basic collistion functionality. might become a real method with new name
    //IEnumerator MoveToFamiliar()
    //{
    //    Vector3 originalPosition = transform.position;
    //    PositionTweener positionTweener = gameObject.GetComponent<PositionTweener>();
    //    Vector3 targetPosition = familiar.transform.position;
    //    Vector3 currentPos = this.transform.position;
    //    var upValue = currentPos.y + 10f;
    //    Vector3 upPosition = new Vector3(currentPos.x, upValue, currentPos.z);
    //    positionTweener.TriggerPosition(upPosition, 18f);
    //    yield return new WaitForSeconds(.5f);
    //    Vector3 overPosition = new Vector3(targetPosition.x, upValue, currentPos.z);
    //    positionTweener.TriggerPosition(overPosition, 18f);
    //    yield return new WaitForSeconds(.5f);

    //    positionTweener.TriggerPosition(targetPosition, 18f);
    //    yield return new WaitForSeconds(1.0f);
    //    positionTweener.TriggerPosition(originalPosition, 18f);
    //}

    // NOTE: evaluate removal of old damage methods below
    //public void ShowDamage()
    //{
    //    StartCoroutine(DamageShake());
    //}

    //IEnumerator DamageShake()
    //{
    //    float shakeDelay = .05f;
    //    float flucation = .5f;
    //    Quaternion startRotation = transform.rotation;
    //    Quaternion damageRotation = Quaternion.Euler(0, 0, -15f);
    //    Vector3 origin = transform.position;
    //    Vector3 leftPos = new Vector3(origin.x + flucation, origin.y, origin.z);
    //    Vector3 rightPos = new Vector3(origin.x - flucation, origin.y, origin.z);

    //    SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    //    sprite.color = Color.red;

    //    transform.rotation = damageRotation;

    //    transform.position = leftPos;
    //    yield return new WaitForSeconds(shakeDelay);
    //    transform.position = rightPos;
    //    yield return new WaitForSeconds(shakeDelay);
    //    transform.position = leftPos;
    //    yield return new WaitForSeconds(shakeDelay);
    //    transform.position = rightPos;
    //    yield return new WaitForSeconds(shakeDelay);
    //    transform.position = origin;
    //    transform.rotation = startRotation;
    //    sprite.color = Color.white;

    //}


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
            // Call the base class damage event invoker method
            DealDamage();
            StartNextAttackCycle();
        }
    }

    public void StartAttack()
    {
        if(!isDead && !victory)
        {
            startAttack.Invoke();
        }
    }

    public void StartNextAttackCycle ()
    {
        canCounter = false;
        StartCoroutine(StartNextAttack());
    }

    IEnumerator StartNextAttack()
    {
        animator?.SetBool(GetTargetAttack(), false);

        yield return new WaitForSeconds(4f);
        animator?.SetBool(GetTargetAttack(), true);

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
