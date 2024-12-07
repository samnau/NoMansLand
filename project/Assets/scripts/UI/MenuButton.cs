using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : SceneButton
{
    protected override void ButtonInit()
    {
        base.ButtonInit();
        targetScene = "BattleDemoMenu";
        button.onClick.AddListener(() => ChangeScene(targetScene));
    }
}
