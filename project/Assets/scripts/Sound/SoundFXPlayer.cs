using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXPlayer : MonoBehaviour
{
    public AudioSource SoundSource;

    public void PlayOneShot(AudioClip TargetSound)
    {
        SoundSource.PlayOneShot(TargetSound);
    }

    public void SetVolume(float targetVolume)
    {
        SoundSource.volume = targetVolume;
    }
}
