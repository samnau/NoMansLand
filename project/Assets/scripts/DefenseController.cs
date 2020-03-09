using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour {
    public bool defense = false;
    string[] keyCombo;

    // Use this for initialization
    void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetGameObject = collision.gameObject;
        //Debug.Log(collision.name);
        if (targetGameObject.tag == "attack")
        {
            keyCombo = targetGameObject.GetComponent<BattleCombos>().defenseCombo;

  //          Debug.Log("defend!");
//            Debug.Log("defense keys:" + keyCombo[0] + keyCombo[1]);
            GetComponentInParent<FightController>().defenseCombo = keyCombo;
            defense = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack")
        {
          //  Debug.Log("defense reset!");
            defense = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
