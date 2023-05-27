using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : BaseCreature
{
    string requiredFamiliar;
    string requiredRune;
    bool hasRequiredFamiliar;
    // may end up being a set list of possibilities
    bool hasRequiredRune;
    [SerializeField] GameObject familiar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
