using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseFamiliar : BaseCreature
{
    public bool canCounter = false;
    bool attackCounterSuccess = false;
    //bool battleChallengeSuccess = false;
    // this should be an event on the Battle UI and not the familiar
    [SerializeField] GameEvent battleChallengeSuccess;

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

    public void BringToFront()
    {
        this.GetComponent<SortingGroup>().sortingLayerID = SortingLayer.NameToID("Player");
    }

    public void SendToBack()
    {
        this.GetComponent<SortingGroup>().sortingLayerID = SortingLayer.NameToID("Familiar");
    }

    // Start is called before the first frame update
    void Start()
    {
       // print($"combo: {defenseCombos[0].keyCode2}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
