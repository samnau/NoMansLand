using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour {
    [HideInInspector]
    public int currentComboIndex = 0;
    [HideInInspector]
    public ComboKeys currentCombo;

    [System.Serializable]
    public class ComboKeys
    {
        [Header("Attack Combo Keys")]
        public string name;
        public string[] defense = new string[2];
        public string[] counterAttack = new string[2];
    }

    [SerializeField]
    public List<ComboKeys> battleCombos;
    // Use this for initialization
    void Start () {
        currentCombo = battleCombos[currentComboIndex];
	}
	
	// Update is called once per frame
	void Update () {
       currentCombo = battleCombos[currentComboIndex];
    }
}
