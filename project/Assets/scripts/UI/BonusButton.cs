using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : SceneButton
{
    protected override void ButtonInit()
    {
        isDisabled = true;
        base.ButtonInit();
        targetScene = "CastleCourtyard";
        button.onClick.AddListener(() => ChangeScene(targetScene));
    }
}
