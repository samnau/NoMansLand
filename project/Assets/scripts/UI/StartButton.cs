using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : SceneButton, IGlobalDataPersistence
{
    [SerializeField]
    ScriptableObject[] objectsToReset;
    ScenePosition scenePosition;
    [SerializeField]
    GameEvent confirmationRequired;
    bool gameInProgress = false;
    MusicController musicController;

    protected override void ButtonInit()
    {
        base.ButtonInit();
        button.onClick.AddListener(StartClickHandler);
    }

    void StartClickHandler()
    {
        if (confirmationRequired && gameInProgress)
        {
            confirmationRequired.Invoke();
            return;
        }
        ResetGameState();
        if (musicController != null)
        {
            musicController.FadeOutMusic();
        }

        //Load the intro scene
        ChangeScene("Intro");
    }

    public void LoadData(GlobalGameData data)
    {
        gameInProgress = data.worldState.gameInProgress;
        musicController = FindObjectOfType<MusicController>();
    }

    public void SaveData(ref GlobalGameData data)
    {
        data.worldState.gameInProgress = gameInProgress;
    }
    public void ResetGameState()
    {
        gameInProgress = true;
        FindObjectOfType<GlobalDataPersistenceManager>().NewGame();
    }

}
