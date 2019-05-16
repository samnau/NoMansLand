using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarActionManager : MonoBehaviour {
    GameObject monster;
    Animator familiar_animator;
    public GameObject lightBeam;
    GameObject attack;
    Vector2 attackStartPosition;

	// Use this for initialization
	void Start () {
        monster = GameObject.FindGameObjectWithTag("Enemy");
        attackStartPosition = new Vector2(monster.transform.position.x, monster.transform.position.y+10.0f);
    }
    public void TriggerAttack ()
    {
        attack = Instantiate(lightBeam, attackStartPosition, transform.rotation);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
