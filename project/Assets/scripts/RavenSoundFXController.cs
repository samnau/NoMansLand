using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenSoundFXController : MonoBehaviour {
    public AudioClip Thump;
    public AudioClip StabStrike;
    public AudioClip Hiss;
    public AudioSource SoundSource;

    void PlayThump()
    {
        PlayOneShot(Thump);
    }

    void PlayStabStrike()
    {
        PlayOneShot(StabStrike);
    }

    void PlayHiss()
    {
        PlayOneShot(Hiss);
    }

    void PlayOneShot(AudioClip TargetSound)
    {
        SoundSource.PlayOneShot(TargetSound);
    }

}
