using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFamiliarSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip croak1;
    [SerializeField] AudioClip croak2;
    [SerializeField] AudioClip croak3;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip slam;
    [SerializeField] AudioClip crash;


    List<AudioClip> croakList;

    private void Start()
    {
        croakList = new List<AudioClip>();
        if(croak1 != null && croak2 != null && croak3 != null)
        {
            croakList.Add(croak1);
            croakList.Add(croak2);
            croakList.Add(croak3);
        }
    }

    public void PlayJump()
    {
        PlayOneShot(jump);
    }
    public void PlaySlam()
    {
        PlayOneShot(slam);
    }
    public void PlayCrash()
    {
        PlayOneShot(crash,.6f);
    }
    public void PlayCroak()
    {
        int targetIndex = Random.Range(0, croakList.Count - 1);
        AudioClip targetCroak = croakList[targetIndex];
        PlayOneShot(targetCroak, .5f);
    }
}
