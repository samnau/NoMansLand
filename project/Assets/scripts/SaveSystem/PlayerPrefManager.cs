using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCurrentScene(string targetScene = "BrokenPool")
    {
        PlayerPrefs.SetString("currentScene", targetScene);
    }

    public string GetCurrentScene()
    {
        return PlayerPrefs.GetString("currentScene");
    }

    public void SetBrokenPoolState(int targetValue = 0)
    {
        PlayerPrefs.SetInt("brokenPoolDialogPlayed", targetValue);
    }

    public int GetBrokenPoolState()
    {
        return PlayerPrefs.GetInt("brokenPoolDialogPlayed");
    }

    public void SetCastleCourtyardState(int targetValue = 0)
    {
        PlayerPrefs.SetInt("castleCourtyardDialogPlayed", targetValue);
    }

    public int GetCastleCourtyardState()
    {
        return PlayerPrefs.GetInt("castleCourtyardDialogPlayed");
    }

    public void SetBonusState(int targetValue = 0)
    {
        PlayerPrefs.SetInt("gameComplete", targetValue);
    }

    public int GetBonusState()
    {
        return PlayerPrefs.GetInt("gameComplete");
    }

    public void SetGameInProgress(int targetValue = 0)
    {
        PlayerPrefs.SetInt("gameInProgress", targetValue);
    }

    public int GetInProgressState()
    {
        return PlayerPrefs.GetInt("gameInProgress");
    }
}
