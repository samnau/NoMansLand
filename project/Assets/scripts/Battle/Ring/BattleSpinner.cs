using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpinner : BattleChallenge
{
    Transform targetTransform;
    float rotationModifier = 1.0f;
    public bool rotationActive = true;
    public float rotationSpeed = 400f;
    float defaultRotationSpeed;
    int successCount = 0;
    public int successLimit = 3;
    [HideInInspector]
    public bool triggerValid = false;
    [SerializeField]
    KeyCode triggerKey = KeyCode.D;
    GameObject battleTrigger;
    GameObject triggerWrapper;
    RotationTweener rotationTweener;

    void Start()
    {
        targetTransform = gameObject.transform;
        battleTrigger = GameObject.FindGameObjectWithTag("BattleTrigger");
        triggerWrapper =  battleTrigger.transform.parent.gameObject;
        rotationTweener = triggerWrapper.GetComponent<RotationTweener>();
        defaultRotationSpeed = rotationSpeed;
        StartCoroutine(Timeout());
    }
    bool IsKeyValid()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
    public void ReverseRotation()
    {
        rotationModifier = rotationModifier * -1f;
    }
    bool SuccessLimitReached()
    {
        return successCount >= successLimit;
    }

    void RotateTriggerWrapper()
    {
        float targetRotation = Random.Range(60f, 270f);
        rotationTweener.TriggerRotation(targetRotation);
//        triggerWrapper.transform.Rotate(0 , 0, targetRotation);
    }
    void CheckForValidTrigger()
    {
        if (triggerValid)
        {
            print($"success: {successCount}");

            if (SuccessLimitReached())
            {
                rotationActive = false;
                battleTrigger.SetActive(false);
                print("you win!!");
            }
            else
            {
                successCount++;
                RotateTriggerWrapper();
                ReverseRotation();
                rotationSpeed += 75f;
            }
        }
        else
        {
            rotationSpeed = defaultRotationSpeed;
            successCount = 0;
            print($"key fail!");
        }
    }
    void SetRotation()
    {
        if (!rotationActive)
        {
            return;
        }
        var targetRotation = rotationSpeed * Time.deltaTime * rotationModifier;
        transform.Rotate(0, 0, targetRotation);
    }

    void Update()
    {
        if(!success && !failure)
        {
            SetRotation();
            if (IsKeyValid())
            {
                CheckForValidTrigger();
            }
        }

    }
}
