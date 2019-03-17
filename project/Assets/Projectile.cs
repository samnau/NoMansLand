using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject player;
    Vector2 currentPosition;
    Vector2 targetPosition;
    public bool KeepLooping = true;
    public float speed = 0.25f;
    float moveIncrement = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentPosition = transform.position;
        FindPlayer();
    }

    void FindPlayer()
    {
        targetPosition = new Vector2(player.transform.position.x, player.transform.position.y+1.0f);
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