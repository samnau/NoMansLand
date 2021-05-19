using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXController : MonoBehaviour {
    public AudioClip FootStepsGrass;
    public AudioClip KeyJangle;
    public AudioClip StaffThump;
    public AudioClip SpellCast;
    public AudioSource SoundSource;
	
	void PlayGrassFootSteps()
    {
        PlayOneShot(FootStepsGrass);
    }

    void PlayStaffThump()
    {
        PlayOneShot(StaffThump);
    }
    void StopGrassFootSteps()
    {
        SoundSource.Stop();
    }

    void PlayOneShot(AudioClip TargetSound)
    {
        SoundSource.PlayOneShot(TargetSound);
    }
    void PlayKeyJangle()
    {
        PlayOneShot(KeyJangle);
    }
    void CastSpell()
    {
        PlayOneShot(SpellCast);
    }

}
