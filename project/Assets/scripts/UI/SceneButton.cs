using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : BaseButton
{
    protected Fade_Controller fadeController;
    [SerializeField]
    protected string targetScene = "Forest1";

    [SerializeField]
    protected LastVisitedScene lastSceneData;

    [SerializeField]
    protected GameState gameStateData;

    void Start()
    {
        ButtonInit();
    }

    protected override void ButtonInit()
    {
        base.ButtonInit();
        fadeController = FindObjectOfType<Fade_Controller>();
    }

    public void ChangeScene(string targetScene)
    {
        fadeController?.triggerLevelChange(targetScene);
        if (lastSceneData == null)
        {
            print("no scene data loaded");
            return;
        }
        lastSceneData.lastScene = targetScene;
    }

}
