using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChallenge : MonoBehaviour
{
    [HideInInspector]
    public bool success = false;
    [HideInInspector]
    public bool failure = false;
    public float timeLimit = 10f;
    public IEnumerator Timeout()
    {
        print("timer started");
        yield return new WaitForSeconds(timeLimit);
        if (!success)
        {
            print("time's up!");
            failure = true;
        }
        else
        {
            print("you win!");
        }
    }
}
