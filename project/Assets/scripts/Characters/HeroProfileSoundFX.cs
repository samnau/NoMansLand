using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProfileSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip staffSlam;
    [SerializeField] AudioClip staffSummon;
    [SerializeField] AudioClip rewind;

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
}
