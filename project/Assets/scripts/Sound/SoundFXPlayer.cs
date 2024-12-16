using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundFXPlayer : MonoBehaviour
{
    public AudioSource SoundSource;
    public bool isEnabled = true;
    float currentVolume;

    void Start()
    {
        currentVolume = SoundSource.volume;
    }

    public void PlayOneShot(AudioClip TargetSound, float targetVolume = 1f)
    {
        SetVolume(targetVolume);
        if(!isEnabled)
        {
            return;
        }
        SoundSource.PlayOneShot(TargetSound);
    }

    void ToggleSounds()
    {
        isEnabled = !isEnabled;
    }

    public void DisableSounds()
    {
        isEnabled = false;
    }

    public void EnableSounds()
    {
        isEnabled = true;
    }

    public void TriggerTimedDisableSounds()
    {
        StartCoroutine(TimedDisableSounds());
    }

    IEnumerator TimedDisableSounds()
    {
        yield return new WaitForSeconds(.5f);
        DisableSounds();
    }

    public void SetVolume(float targetVolume)
    {
        SoundSource.volume = targetVolume;
    }

    public void UnMuteSounds()
    {
        SoundSource.volume = currentVolume;
    }

    public void MuteSounds()
    {
        currentVolume = SoundSource.volume;
        SoundSource.volume = 0;
    }

    public void StopPlayback()
    {
        SoundSource.Stop();
    }
}
