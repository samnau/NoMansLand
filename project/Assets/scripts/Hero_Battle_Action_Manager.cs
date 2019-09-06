using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Battle_Action_Manager : MonoBehaviour {
    Animator hero_animator;
    public GameObject shield_spell;
    //GameObject monsterHealthText;
    GameObject monster;
    MonsterHealthManager monsterHealthTracker;
    monster_action_manager monsterActionManager;
    GameObject test_defense;
    float changeIncrement = 0;
    bool fadeEnabled = false;
    bool canAttack = false;
    bool attackComoboPressed = false;
    GameObject familiar;
    FamiliarActionManager familiarActionManager;
    Vector2 familiarPosition;
    Key_Validator attackComboValidator;
    Monster monsterClass;
    Monster.ComboKeys currentCombo;
    private int comboCount;

    // Use this for initialization
    void Start () {
        hero_animator = gameObject.GetComponent<Animator>();
        familiar = GameObject.FindGameObjectWithTag("familiar");
        familiarActionManager = familiar.GetComponent<FamiliarActionManager>();
        attackComboValidator = familiar.GetComponent<Key_Validator>();
        findFamiliarPosition();
        hero_animator.SetBool("RIGHT", true);
        //monsterHealthText = GameObject.Find("monster_health_indicator");
        // find the monster current combo keys
        monster = GameObject.FindGameObjectWithTag("Enemy");
        monsterClass = monster.GetComponent<Monster>();
        currentCombo = monsterClass.currentCombo;
        attackComboValidator.keyCombo = currentCombo.counterAttack;
        monsterHealthTracker = monster.GetComponent<MonsterHealthManager>();
        monsterActionManager = monster.GetComponent<monster_action_manager>();
        comboCount = monsterClass.battleCombos.Count - 1;
    }
    
    void UpdateCounterAttack()
    {
        if(monsterClass.currentComboIndex < comboCount)
        {
            monsterClass.currentComboIndex++;
        }
        currentCombo = monsterClass.battleCombos[monsterClass.currentComboIndex];
        attackComboValidator.keyCombo = currentCombo.counterAttack;
        Debug.Log("attack manager 2nd attack key: " + currentCombo.counterAttack[0]);
    }

    void setCanAttack()
    {
        canAttack = monsterActionManager.heroCanAttack;
    }
    void TriggerAttack ()
    {
        monsterHealthTracker.TakeDamage();
        familiarActionManager.TriggerAttack();
        monsterActionManager.heroCanAttack = false;
        UpdateCounterAttack();
    }
	public void TriggerDefense()
    {
        hero_animator.SetBool("ACTION", true);
        StartCoroutine(ShieldSpell());
    }
    void findFamiliarPosition()
    {
        familiarPosition = familiar.transform.position;
    }
    IEnumerator FadeInShield()
    {
        var targetColor = new Color(1f, 1f, 1f, 1f);
        var shieldColorMaterial = test_defense.GetComponent<SpriteRenderer>().material;

        while (fadeEnabled)
        {
            if (changeIncrement < 1.0f)
            {
                changeIncrement += (Time.deltaTime * 1.5f);
                var currentColor = shieldColorMaterial.color;
                shieldColorMaterial.color = Color.Lerp(currentColor, targetColor, changeIncrement);
                yield return new WaitForSeconds(.05f);
            }else
            {
                fadeEnabled = false;
            }
        }
    }
    IEnumerator ShieldSpell()
    {
        yield return new WaitForSeconds(0.25f);
        float shieldScale = 1.05f;
        var targetPositon = new Vector2(familiarPosition.x+0.25f, familiarPosition.y + 3.0f);
        test_defense = Instantiate(shield_spell, targetPositon, transform.rotation);
        test_defense.transform.localScale = new Vector2(shieldScale, shieldScale);
        changeIncrement = 0;
        test_defense.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
        fadeEnabled = true;
        StartCoroutine(FadeInShield());
        yield return new WaitForSeconds(0.6f);
        Destroy(test_defense);
        hero_animator.SetBool("ACTION", false);
    }
	// Update is called once per frame
	void Update () {
        setCanAttack();
        attackComoboPressed = attackComboValidator.comboPressed;
        if (attackComoboPressed && canAttack)
        {
            TriggerAttack();
        }
	}
}
