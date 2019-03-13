using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Battle_Action_Manager : MonoBehaviour {
    Animator hero_animator;
    public GameObject shield_spell;
    GameObject test_defense;
	// Use this for initialization
	void Start () {
        hero_animator = gameObject.GetComponent<Animator>();
        hero_animator.SetBool("RIGHT", true);
	}
	public void TriggerDefense()
    {
        hero_animator.SetBool("ACTION", true);
        StartCoroutine(ShieldSpell());
    }
    IEnumerator ShieldSpell()
    {
        yield return new WaitForSeconds(0.25f);
        var targetPositon = new Vector2(transform.position.x, transform.position.y + 1.0f);
        test_defense = Instantiate(shield_spell, targetPositon, transform.rotation);
        yield return new WaitForSeconds(0.6f);
        Destroy(test_defense);
        hero_animator.SetBool("ACTION", false);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
