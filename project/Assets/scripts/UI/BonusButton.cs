using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : SceneButton
{
    protected override void ButtonInit()
    {
        isDisabled = !gameStateData.gameComplete;
        base.ButtonInit();
        targetScene = "CastleCourtyard";
        button.onClick.AddListener(() => ChangeScene(targetScene));
    }
}
