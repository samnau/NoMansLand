using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundFXPlayer : MonoBehaviour
{
    public AudioSource SoundSource;

    public void PlayOneShot(AudioClip TargetSound, float targetVolume = 1f)
    {
        SetVolume(targetVolume);
        SoundSource.PlayOneShot(TargetSound);
    }

    public void SetVolume(float targetVolume)
    {
        SoundSource.volume = targetVolume;
    }
}
