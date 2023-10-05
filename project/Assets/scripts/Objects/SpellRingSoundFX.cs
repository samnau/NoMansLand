using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRingSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip appear1;
    [SerializeField] AudioClip glide;

    public void PlayAppear1()
    {
        PlayOneShot(appear1);
    }
    public void PlayGlide()
    {
        PlayOneShot(glide);
    }
}
