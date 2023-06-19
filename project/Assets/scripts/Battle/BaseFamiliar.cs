using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFamiliar : BaseCreature
{
    public bool canCounter = false;
    bool attackCounterSuccess = false;
    //bool battleChallengeSuccess = false;
    // this should be an event on the Battle UI and not the familiar
    [SerializeField] GameEvent battleChallengeSuccess;

    // Start is called before the first frame update
    void Start()
    {
       // print($"combo: {defenseCombos[0].keyCode2}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
