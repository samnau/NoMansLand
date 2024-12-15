using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : SceneButton
{
    //protected override void ButtonInit()
    //{
    //    base.ButtonInit();
    //    fadeController = FindObjectOfType<Fade_Controller>();
    //    targetScene = "BattleDemoMenu";
    //    print("menu btn init");

    //    button.onClick.AddListener(() => ChangeScene(targetScene));
    //}

    private void OnEnable()
    {
        ButtonInit();
    }

    protected override void SceneButtonInit()
    {
        initialized = true;
        base.SceneButtonInit();
        targetScene = "BattleDemoMenu";

        button.onClick.AddListener(MenuClickHandler);
    }

    void MenuClickHandler()
    {
        FindObjectOfType<PauseMenuController>().TogglePausePanel();
        ChangeScene(targetScene);
    }
}
