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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayRingAppears()
    {
        PlayOneShot(ringAppear);
    }
    public void PlaySpellFailure()
    {
        PlayOneShot(failedSpell);
    }
    public void PlaySpellSuccess()
    {
        PlayOneShot(successSpell);
    }
    public void PlayRuneMiss()
    {
        PlayOneShot(failedHit);
    }
    public void PlayRuneHit1()
    {
        PlayOneShot(score1);
    }
    public void PlayRuneHit2()
    {
        PlayOneShot(score2);
    }
    public void PlayRuneHit3()
    {
        PlayOneShot(score3);
    }
    public void PlayCountDown()
    {
        StartCoroutine(PlayCountDownSequence(.5f));
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
