using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Battle_Action_Manager : MonoBehaviour {
    Animator hero_animator;
    public GameObject shield_spell;
    GameObject monsterHealthText;
    MonsterHealthManager monsterHealthTracker;
    GameObject test_defense;
    float changeIncrement = 0;
    bool fadeEnabled = false;
    Vector2 familiarPosition;
    // Use this for initialization
    void Start () {
        hero_animator = gameObject.GetComponent<Animator>();
        findFamiliarPosition();
        hero_animator.SetBool("RIGHT", true);
        monsterHealthText = GameObject.Find("monster_health_indicator");
        monsterHealthTracker = monsterHealthText.GetComponent<MonsterHealthManager>();
	}
    void TriggerAttack ()
    {
        monsterHealthTracker.TakeDamage();
    }
	public void TriggerDefense()
    {
        hero_animator.SetBool("ACTION", true);
        StartCoroutine(ShieldSpell());
    }
    void findFamiliarPosition()
    {
        var familiar = GameObject.FindGameObjectWithTag("familiar");
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
        var targetPositon = new Vector2(familiarPosition.x, familiarPosition.y + 1.0f);
        //var targetPositon = new Vector2(transform.position.x, transform.position.y + 1.0f);
        test_defense = Instantiate(shield_spell, targetPositon, transform.rotation);
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
        if (Input.GetKeyDown("a"))
        {
            TriggerAttack();
        }
	}
}
