using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip grassFootsteps;
    [SerializeField] AudioClip stoneFootsteps;

    public void PlayGrassFootstep()
    {
        PlayOneShot(grassFootsteps, .5f);
    }

    public void PlayStoneFootstep()
    {
        PlayOneShot(stoneFootsteps);
    }
}
