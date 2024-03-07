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
    [SerializeField] AudioClip skewer;
    [SerializeField] AudioClip stab;
    [SerializeField] AudioClip rise;
    [SerializeField] AudioClip tongueStab;
    [SerializeField] AudioClip screamStab;
    [SerializeField] AudioClip rockRumble;
    [SerializeField] AudioClip gag1;
    [SerializeField] AudioClip gag2;
    [SerializeField] AudioClip gag3;
    [SerializeField] AudioClip boom;
    [SerializeField] AudioClip energyBuildup;
    [SerializeField] AudioClip laser1;
    [SerializeField] AudioClip death;


    float gagVolume = .2f;

    public void PlayRustle()
    {
        PlayOneShot(rustle, .2f);
    }

    public void PlayRockRumble()
    {
        PlayOneShot(rockRumble, .7f);
    }

    public void PlayTongueStab()
    {
        PlayOneShot(tongueStab);
    }

    public void PlayScreamStab()
    {
        PlayOneShot(screamStab);
    }

    public void PlayStab()
    {
        PlayOneShot(stab);
    }

    public void PlaySkewer()
    {
        PlayOneShot(skewer, .75f);
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
        PlayOneShot(gag3, gagVolume);
    }

    public void PlayDeath()
    {
        PlayOneShot(death);
    }

    public void PlayBoom()
    {
        PlayOneShot(boom, .6f);
    }

    public void PlayEnergyBuildup()
    {
        PlayOneShot(energyBuildup);
    }

    public void PlayLaser1()
    {
        PlayOneShot(laser1);
    }
}
