using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSweeperController : BattleChallenge
{
    RotationTweener rotationTweener;
    RectTransform targetTransform;
    bool canSweep = true;

    void Start()
    {
        rotationTweener = gameObject.GetComponent<RotationTweener>();
        targetTransform = gameObject.GetComponent<RectTransform>();
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
            print($"canvas rotation: {targetTransform.eulerAngles.z}");
            rotationTweener.TriggerRotation(targetAngle, 4f);
            StartCoroutine(InputGuard());
        }
    }

    void Update()
    {
        if(Input.anyKeyDown && canSweep)
        {
            CheckForInput();
        }
    }
}
