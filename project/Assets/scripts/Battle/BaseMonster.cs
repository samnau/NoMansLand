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

    public void TriggerAnimationPause(float pauseDuration = .25f)
    {
        StartCoroutine(PauseAnimation(pauseDuration));
    }

    public void TriggerSpeedChange(float targetSpeed = 1f)
    {
        StartCoroutine(AlterAnimationSpeed(targetSpeed));
    }

    IEnumerator PauseAnimation(float pauseDuration = .25f)
    {
        animator.speed = 0;
        yield return new WaitForSeconds(pauseDuration);
        animator.speed = 1f;
    }

    IEnumerator AlterAnimationSpeed(float targetSpeed = 1f, float duration = .75f)
    {
        animator.speed = targetSpeed;
        yield return new WaitForSeconds(duration);
        animator.speed = 1f;
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

            foreach(string attckTrigger in attackTriggers)
            {
                animator?.SetBool(attckTrigger, false);
            }
            if (canDefend)
            {
                defenseSuccess.Invoke();
                battleChallengeStart.Invoke();
                canCounter = true;
            }

        }

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
