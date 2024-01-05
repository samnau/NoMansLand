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
    [SerializeField] AudioClip clickClack;
    [SerializeField] AudioClip singleClack;
    [SerializeField] AudioClip stab;
    [SerializeField] AudioClip rise;
    [SerializeField] AudioClip tongueStab;
    [SerializeField] AudioClip gag1;
    [SerializeField] AudioClip gag2;
    [SerializeField] AudioClip gag3;

    float gagVolume = .4f;

    public void PlayRustle()
    {
        PlayOneShot(rustle, .2f);
    }

    public void PlayTongueStab()
    {
        PlayOneShot(tongueStab);
    }

    public void PlayStab()
    {
        PlayOneShot(stab);
    }

    public void PlayCrash()
    {
        PlayOneShot(crash);
    }
    public void PlayHum()
    {
        PlayOneShot(hum);
    }

    public void PlayRise()
    {
        PlayOneShot(rise, .5f);
    }
    public void PlayCrack1()
    {
        PlayOneShot(legCrack1, .6f);
    }

    public void PlayRoar2()
    {
        PlayOneShot(roar2, .5f);
    }

    public void PlayCrack2()
    {
        PlayOneShot(legCrack2, .6f);
    }

    public void PlayClickClack()
    {
        PlayOneShot(clickClack, .2f);
    }

    public void PlaySingleClack()
    {
        PlayOneShot(singleClack, .5f);
    }

    public void PlayGag1()
    {
        PlayOneShot(gag1, gagVolume);
    }

    public void PlayGag2()
    {
        PlayOneShot(gag2, gagVolume);
    }

    public void PlayGag3()
    {
        PlayOneShot(gag3, .6f);
    }
}
