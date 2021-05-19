using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    public bool damage = false;
    // Use this for initialization
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack")
        {
            Debug.Log("hit!");
            damage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "attack")
        {
            Debug.Log("done!");
            damage = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
