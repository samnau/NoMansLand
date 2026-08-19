using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySoundFX : SoundFXPlayer
{
    [SerializeField] AudioClip inventoryShow;
    [SerializeField] AudioClip inventoryHide;
    [SerializeField] AudioClip inventoryHover;
    [SerializeField] AudioClip inventorySelect;
 
    public void PlayShow()
    {
        if(inventoryShow == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(inventoryShow, 0.4f);
    }

    public void PlayHide()
    {
        if (inventoryHide == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(inventoryHide);
    }

    public void PlayHover()
    {
        if (inventoryHover == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(inventoryHover, 0.3f);
    }

    public void PlaySelect()
    {
        if (inventorySelect == null)
        {
            print("No sound assigned");
            return;
        }
        PlayOneShot(inventorySelect, 0.3f);
    }
}
