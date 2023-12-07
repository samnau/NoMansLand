using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseFamiliar : BaseCreature
{
    public bool canCounter = false;
    public bool summoned = true;
    bool attackCounterSuccess = false;
    //bool battleChallengeSuccess = false;
    // this should be an event on the Battle UI and not the familiar
    [SerializeField] GameEvent battleChallengeSuccess;

    SpriteRenderer[] allSprites;
    Color[] defaultColors;

    public void ShowDamage()
    {
        StartCoroutine(DamageShake());
    }

    IEnumerator DamageShake()
    {
        yield return new WaitForSeconds(.25f);

        float shakeDelay = .05f;
        float flucation = .5f;
        Vector3 origin = transform.position;
        Vector3 leftPos = new Vector3(origin.x + flucation, origin.y, origin.z);
        Vector3 rightPos = new Vector3(origin.x - flucation, origin.y, origin.z);
        SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;

        transform.position = leftPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = rightPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = leftPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = rightPos;
        yield return new WaitForSeconds(shakeDelay);
        transform.position = origin;
        sprite.color = Color.white;
    }
    // TODO: need array of alphas to account for colors with transparency - see white sprite setter
    void ToggleSprites(bool hideSprites = true)
    {
        //float newAlpha = hideSprites ? 0f : 255f;
        //foreach (SpriteRenderer sprite in allSprites)
        //{
        //    var newColor = sprite.color;
        //    newColor.a = newAlpha;
        //    if(!hideSprites)
        //    {
        //        defaultColors(sprite);
        //    }
        //    sprite.color = newColor;
        //}

        for (int i = 0; i < allSprites.Length; i++)
        {
            var newColor = allSprites[i].color;
            newColor.a = 0;
            if (!hideSprites)
            {
                newColor = defaultColors[i];
            }
            allSprites[i].color = newColor;
        }
    }
    public void HideFamiliar()
    {
        print("hide the familiar");
        ToggleSprites(true);
    }

    public void ShowFamiliar()
    {
        ToggleSprites(false);
        this.GetComponent<Animator>().SetBool("HIDDEN", false);
    }

    public void BringToFront()
    {
        SortingGroup sortingGroup = this.GetComponent<SortingGroup>();
        sortingGroup.sortingLayerID = SortingLayer.NameToID("Creatures");
        sortingGroup.sortingOrder = 100;
    }

    public void SendToBack()
    {
        SortingGroup sortingGroup = this.GetComponent<SortingGroup>();

        sortingGroup.sortingLayerID = SortingLayer.NameToID("Familiar");
        sortingGroup.sortingOrder = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        allSprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        defaultColors = new Color[allSprites.Length];
        for (int i = 0; i < allSprites.Length; i++)
        {
            defaultColors[i] = allSprites[i].color;
        }
        if (summoned)
        {
            ShowFamiliar();
        } else
        {
            HideFamiliar();
        }
    }

}
