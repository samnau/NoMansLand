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

    void Start()
    {
        familiar = GameObject.FindGameObjectWithTag("familiar");
        currentPosition = transform.position;
        FindFamiliar();
    }

    void FindFamiliar()
    {
        targetPosition = new Vector2(familiar.transform.position.x, familiar.transform.position.y + 0.25f);
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