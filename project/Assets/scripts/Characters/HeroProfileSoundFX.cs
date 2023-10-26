using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProfileSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip staffSlam;

    public void PlayStaffSlam()
    {
        PlayOneShot(staffSlam, 0.125f);
    }

}
