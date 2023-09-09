using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFrogAttack : MonoBehaviour
{
    [SerializeField] GameEvent familiarDealDamage;

    public void DealDamage()
    {
        familiarDealDamage.Invoke();
    }

}
