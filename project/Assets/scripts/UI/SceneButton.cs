using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : BaseButton
{
    protected Fade_Controller fadeController;
    [SerializeField]
    string targetScene = "Forest1";
    void Start()
    {
        fadeController = FindObjectOfType<Fade_Controller>();
    }

    public void ChangeScene(string targetScene)
    {
        fadeController?.triggerLevelChange(targetScene);
    }

}
