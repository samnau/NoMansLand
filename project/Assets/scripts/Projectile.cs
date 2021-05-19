using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject familiar;
    Vector2 currentPosition;
    Vector2 targetPosition;
    public bool KeepLooping = true;
    public float speed = 0.25f;
    float moveIncrement = 0;
    GameObject monster;
    monster_action_manager monsterManager;

    void Start()
    {
        familiar = GameObject.FindGameObjectWithTag("familiar");
        currentPosition = transform.position;
        FindFamiliar();
        monster = GameObject.FindGameObjectWithTag("Enemy");
        monsterManager = monster.GetComponent<monster_action_manager>();
    }

    void FindFamiliar()
    {
        targetPosition = new Vector2(familiar.transform.position.x + 0.5f, familiar.transform.position.y + 3.25f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("familiar"))
        {
            StartCoroutine(monsterManager.OpenDefenseWindow());
            Debug.Log("defend!");
        }
    }
    void findProjectilePosition()
    {
        currentPosition = transform.position;
    }

    void MoveProjectile()
    {
        moveIncrement += (Time.deltaTime * speed);
        findProjectilePosition();
        if(moveIncrement < 1.0f)
        {
            transform.position = Vector2.Lerp(currentPosition, targetPosition, moveIncrement);
        }
    }

    void FixedUpdate()
    {
        MoveProjectile();
    }
}