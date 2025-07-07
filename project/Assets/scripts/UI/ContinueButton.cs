using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : SceneButton, IGlobalDataPersistence
{
    protected override void ButtonInit()
    {
        prefManager = FindObjectOfType<PlayerPrefManager>();
        //isHidden = prefManager.GetInProgressState() == 0;
        if(!initialized)
        {
            isHidden = false;
        }
        //isHidden = gameStateData != null && !gameStateData.gameInProgress;
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
        //string sceneName = prefManager?.GetCurrentScene();

        ChangeScene(targetScene);
    }

    public void LoadData(GlobalGameData data)
    {
        targetScene = data.worldState.currentScene;
        isHidden = !data.worldState.gameInProgress;
        print($"continue button loaded scene name: {targetScene}");
        print($"continue button in progress state: {isHidden}");
        ButtonInit();
    }

    public void SaveData(ref GlobalGameData data)
    {
        // no save needed
    }

}
