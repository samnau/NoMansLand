using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShieldSoundFXController : MonoBehaviour {
    public AudioClip Ignite;
    public AudioClip Burn;
    public AudioClip Appear;
    public AudioSource SoundSource;

    void PlayIgnite()
    {
        PlayOneShot(Ignite);
    }

    void PlayBurn()
    {
        PlayOneShot(Burn);
    }

    void PlayAppear()
    {
        PlayOneShot(Appear);
    }

    void PlayOneShot(AudioClip TargetSound)
    {
        SoundSource.PlayOneShot(TargetSound);
    }
}
