using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAnimationSoundFX : SoundFXPlayer
{
    [SerializeField]
    AudioClip ringAppear;
    [SerializeField]
    AudioClip score1;
    [SerializeField]
    AudioClip score2;
    [SerializeField]
    AudioClip score3;
    [SerializeField]
    AudioClip failedHit;
    [SerializeField]
    AudioClip failedSpell;
    [SerializeField]
    AudioClip successSpell;

    [SerializeField]
    AudioClip countdown1;
    [SerializeField]
    AudioClip countdown2;
    [SerializeField]
    AudioClip countdown3;

    [SerializeField]
    AudioClip hitSuccess;


    float orbitRingVolume = 0.1f;

    public void PlayRingAppears()
    {
        PlayOneShot(ringAppear, .6f);
    }
    public void PlaySpellFailure()
    {
        PlayOneShot(failedSpell, .5f);
    }
    public void PlaySpellSuccess()
    {
        PlayOneShot(successSpell, .5f);
    }
    public void PlayRuneMiss()
    {
        PlayOneShot(failedHit,.2f);
    }
    public void PlayRuneHit1()
    {
        PlayOneShot(score1, orbitRingVolume);
    }
    public void PlayRuneHit2()
    {
        PlayOneShot(score2, orbitRingVolume);
    }
    public void PlayRuneHit3()
    {
        PlayOneShot(score3, orbitRingVolume);
    }
    public void PlayCountDown()
    {
        StartCoroutine(PlayCountDownSequence(1f));
    }
    public void PlayHitSuccess()
    {
        PlayOneShot(hitSuccess, .8f);
    }

    IEnumerator PlayCountDownSequence(float interval)
    {
        PlayOneShot(countdown1);
        yield return new WaitForSeconds(interval);
        PlayOneShot(countdown2);
        yield return new WaitForSeconds(interval);
        PlayOneShot(countdown3);
    }
}
