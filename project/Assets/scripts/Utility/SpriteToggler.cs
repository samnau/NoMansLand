using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpriteToggler : MonoBehaviour
{
    [SerializeField] bool toggleOnStart = false;
    SpriteRenderer[] allSprites;

    private void Start()
    {
        if(toggleOnStart)
        {
            ToggleSprites();
        }
    }
    public void ToggleSprites([Optional] GameObject targetParent)
    {
        allSprites = targetParent != null ? targetParent.GetComponentsInChildren<SpriteRenderer>() : this.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < allSprites.Length; i++)
        {
            allSprites[i].enabled = !allSprites[i].enabled;
        }
    }

}
