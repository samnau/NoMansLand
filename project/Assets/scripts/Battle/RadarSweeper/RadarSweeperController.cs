using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSweeperController : BattleChallenge
{
    RotationTweener rotationTweener;
    RectTransform targetTransform;
    public bool canSweep = true;
    RuneIntroSequencer runeIntroSequencer;

    void Start()
    {
        rotationTweener = gameObject.GetComponent<RotationTweener>();
        targetTransform = gameObject.GetComponent<RectTransform>();
        runeIntroSequencer = FindObjectOfType<RuneIntroSequencer>();
    }

    IEnumerator InputGuard()
    {
        canSweep = false;
        yield return new WaitForSeconds(.3f);
        canSweep = true;
    }

    void CheckForInput()
    {
        var rotationModifier = 1f;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotationModifier = 1f;
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotationModifier = -1f;
        }

        var targetAngle = targetTransform.eulerAngles.z + (90f * rotationModifier);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //rotationTweener.TriggerRotation(targetAngle, 4f);
            rotationTweener.TriggerRotation(targetAngle, .5f);
            StartCoroutine(InputGuard());
        }
    }

    void Update()
    {
        if(Input.anyKeyDown && canSweep && !runeIntroSequencer.exitAnimationStarted)
        {
            CheckForInput();
        }
    }
}
