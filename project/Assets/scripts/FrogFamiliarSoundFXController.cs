using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFamiliarSoundFXController : MonoBehaviour {
    public AudioClip SmallCroak;
    public AudioClip BigCroak;
    public AudioClip Spray;
    public AudioSource SoundSource;

    void PlaySmallCroak()
    {
        PlayOneShot(SmallCroak);
    }

    void PlayBigCraok()
    {
        PlayOneShot(BigCroak);
    }

    void PlaySpray()
    {
        PlayOneShot(Spray);
    }

    void PlayOneShot(AudioClip TargetSound)
    {
        SoundSource.PlayOneShot(TargetSound);
        SoundSource.volume = .2f;
    }
}
