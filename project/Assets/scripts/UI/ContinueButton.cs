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
        print("continue click");
        if(lastSceneData == null)
        {
            print("no scene data loaded");
            return;
        }
        string sceneName = lastSceneData?.lastScene;
        ChangeScene(sceneName);
    }

}
