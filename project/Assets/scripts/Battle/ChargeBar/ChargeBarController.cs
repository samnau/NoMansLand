using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarController : BattleChallenge
{
    float currentX;
    float currentY;
    float minY;
    RectTransform targetTransform;

    void Start()
    {
        targetTransform = gameObject.GetComponent<RectTransform>();
        SetCurrentY();
        currentX = targetTransform.anchoredPosition.x;
        float height = targetTransform.rect.height;
        float startPosY = minY = (height) * -1f;
        targetTransform.anchoredPosition = new Vector2(currentX, startPosY);

        StartCoroutine(Timeout());
    }

    void SetCurrentY()
    {
        currentY = targetTransform.anchoredPosition.y;
    }

    void IncreaseYPosition()
    {
        if(currentY < Vector2.zero.y)
        {
            targetTransform.Translate(Vector2.up * Time.deltaTime * 20f);
        }
    }

    void DecreaseYPosition()
    {
        if (currentY > minY)
        {
            targetTransform.Translate(Vector2.down * Time.deltaTime * 1f);
        }
    }

    void CheckForChargeSuccess()
    {
        if(currentY >= 0f)
        {
            targetTransform.anchoredPosition = new Vector2(currentX, 0);
            success = true;
            print("bar charged!");
        }
    }

    void TriggerBarCharge()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseYPosition();
        } else
        {
            return;
        }
    }

    void Update()
    {
        if (!success && !failure)
        {
            SetCurrentY();
            CheckForChargeSuccess();
            TriggerBarCharge();
            DecreaseYPosition();
        }
    }
}
