using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProfileSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip staffSlam;
    [SerializeField] AudioClip staffSummon;
    [SerializeField] AudioClip rewind;
    [SerializeField] AudioClip fallCrash;
    [SerializeField] AudioClip longOw;
    [SerializeField] AudioClip hurtImpact;
    [SerializeField] AudioClip fallingScream;

    public void PlayStaffSlam()
    {
        PlayOneShot(staffSlam, 0.125f);
    }

    public void PlayStaffSummon()
    {
        PlayOneShot(staffSummon, .6f);
    }

    public void PlayRewind()
    {
        PlayOneShot(rewind, 1f);
    }

    public void PlayFallCrash()
    {
        if (fallCrash == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(fallCrash, .8f);
    }

    public void PlayLongOw()
    {
        if (longOw == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(longOw, .1f);
    }

    public void PlayHurtImpact()
    {
        if (hurtImpact == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(hurtImpact, .2f);
    }

    public void PlayFallingScream()
    {
        if (fallingScream == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(fallingScream, .8f);
    }
}
