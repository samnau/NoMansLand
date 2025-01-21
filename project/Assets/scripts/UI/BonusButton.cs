using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : SceneButton
{
    protected override void ButtonInit()
    {
        prefManager = FindObjectOfType<PlayerPrefManager>();
        if (prefManager != null)
        {
            isDisabled = prefManager.GetBonusState() != 1;
        } else
        {
            isDisabled = true;
        }
        base.ButtonInit();
        targetScene = "CastleCourtyard";
        button.onClick.AddListener(() => ChangeScene(targetScene));
    }
}
