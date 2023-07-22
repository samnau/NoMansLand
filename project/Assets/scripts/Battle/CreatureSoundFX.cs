using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip damage;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip attack1;
    [SerializeField] AudioClip attack2;
    [SerializeField] AudioClip attack3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage()
    {
        PlayOneShot(damage);
    }

    public void Death()
    {
        PlayOneShot(death);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
