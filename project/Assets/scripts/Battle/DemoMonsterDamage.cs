using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMonsterDamage : MonoBehaviour
{
    public void ShowDamage()
    {
        this.GetComponent<Animator>().SetBool("DAMAGE", true);
        StartCoroutine(HideDamage());
    }

    IEnumerator HideDamage()
    {
        yield return new WaitForSeconds(.25f);
        this.GetComponent<Animator>().SetBool("DAMAGE", false);
    }
}
