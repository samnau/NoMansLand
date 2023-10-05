using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRingController : MonoBehaviour
{
    [SerializeField] GameObject hero;
    [SerializeField] GameObject familiar;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        StartCoroutine(TestAppear());
    }

    public void Appear1()
    {
        animator.SetBool("APPEAR1", true);
    }

    public void ShowFamiliar()
    {
        familiar.GetComponent<Animator>().SetBool("SUMMONED", true);
    }


    IEnumerator TestAppear()
    {
        yield return new WaitForSeconds(3f);
        Appear1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
