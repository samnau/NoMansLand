using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProfileSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip staffSlam;
    [SerializeField] AudioClip staffSummon;

    public void PlayStaffSlam()
    {
        PlayOneShot(staffSlam, 0.125f);
    }

    public void PlayStaffSummon()
    {
        PlayOneShot(staffSummon, .6f);
    }

}
