using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : SceneButton
{

    //private void OnEnable()
    //{
    //    ButtonInit();
    //}

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
