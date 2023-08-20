using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSpiderSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip rustle;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip legCrack1;
    [SerializeField] AudioClip legCrack2;
    [SerializeField] AudioClip roar1;
    [SerializeField] AudioClip roar2;
    [SerializeField] AudioClip hum;

    public void PlayRustle()
    {
        PlayOneShot(rustle);
    }

    public void PlayCrash()
    {
        PlayOneShot(crash);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
