using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : SceneButton
{
    protected override void ButtonInit()
    {
        isHidden = gameStateData != null && !gameStateData.gameInProgress;
        base.ButtonInit();
        button.onClick.AddListener(() => LoadLastVisitedScene());
    }
    void LoadLastVisitedScene()
    {
        if (prefManager == null)
        {
            print("no save data loaded");
            return;
        }
        string sceneName = prefManager?.GetCurrentScene();

        ChangeScene(sceneName);
    }

}
