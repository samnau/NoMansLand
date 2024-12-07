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
    // notes: add method that compares the default scene with the last visited scene. probably also need a bool flag for if start button was clicked.
    // maybe bonus button does not set last scene
    void LoadLastVisitedScene()
    {
        if(lastSceneData == null)
        {
            print("no scene data loaded");
            return;
        }
        string sceneName = lastSceneData?.lastScene;
        ChangeScene(sceneName);
    }

}
