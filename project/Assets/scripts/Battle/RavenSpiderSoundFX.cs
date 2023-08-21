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
        PlayOneShot(rustle, .2f);
    }

    public void PlayCrash()
    {
        PlayOneShot(crash);
    }
    public void PlayHum()
    {
        PlayOneShot(hum);
    }
    public void PlayCrack1()
    {
        PlayOneShot(legCrack1, .6f);
    }

    public void PlayCrack2()
    {
        PlayOneShot(legCrack2, .6f);
    }

 
}
