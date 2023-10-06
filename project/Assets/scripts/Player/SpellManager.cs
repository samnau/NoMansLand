using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] GameObject spellRing;

    public void StartSummon()
    {
        spellRing?.GetComponent<SpellRingController>().Appear1();
    }
}
