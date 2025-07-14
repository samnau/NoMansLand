using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : SceneButton, IGlobalDataPersistence
{
    public void LoadData(GlobalGameData data)
    {
        isDisabled = !data.worldState.gameComplete;
        ButtonInit();
    }

    public void SaveData(ref GlobalGameData data)
    {
        // no save needed
    }
    protected override void ButtonInit()
    {
        //prefManager = FindObjectOfType<PlayerPrefManager>();
        //if (prefManager != null)
        //{
        //    isDisabled = prefManager.GetBonusState() != 1;
        //} else
        //{
        //    isDisabled = true;
        //}
        base.ButtonInit();
        targetScene = "CastleCourtyard";
        button.onClick.AddListener(() => ChangeScene(targetScene));
    }
}
