using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRingTrigger : MonoBehaviour
{
    BattleSpinner battleSpiner;

    void Start()
    {
        battleSpiner = FindObjectOfType<BattleSpinner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger)
        {
            battleSpiner.triggerValid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger)
        {
            battleSpiner.triggerValid = false;
        }
    }
}
