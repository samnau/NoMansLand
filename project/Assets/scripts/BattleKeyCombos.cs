using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleKeyCombos : MonoBehaviour {
    public KeyCode[] defenseCombo = new KeyCode[] { KeyCode.W, KeyCode.UpArrow };
    public KeyCode[] counterAttackCombo = new KeyCode[] { KeyCode.A, KeyCode.RightArrow };
    public bool activeAttack = true;
    private void Start()
    {
        
    }
}
