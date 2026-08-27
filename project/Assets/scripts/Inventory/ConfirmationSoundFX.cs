using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationSoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip selectSound;
    // Start is called before the first frame update
    public void PlayHover()
    {
        if (hoverSound == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(hoverSound);
    }

    public void PlaySelect()
    {
        if (selectSound == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(selectSound);
    }
}
