using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : SceneButton
{
    protected override void ButtonInit()
    {
        //isDisabled = !gameStateData.gameComplete;
        if(gameStateManager != null)
        {
            isDisabled = !gameStateManager.gameComplete;
        } else
        {
            isDisabled = true;
        }
        base.ButtonInit();
        targetScene = "CastleCourtyard";
        button.onClick.AddListener(() => ChangeScene(targetScene));
    }
}
