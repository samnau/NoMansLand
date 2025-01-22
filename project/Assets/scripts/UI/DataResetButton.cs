using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataResetButton : BaseButton
{
    //NOTE: this is only used to reset the demo save data before a release build
    protected override void ButtonInit()
    {
        base.ButtonInit();
        button.onClick.AddListener(() => ResetPlayerPrefData());
    }

    public void ResetPlayerPrefData()
    {
        PlayerPrefs.DeleteAll();

        prefManager.SetBrokenPoolState(0);
        prefManager.SetCastleCourtyardState(0);
        prefManager.SetBonusState(0);
        prefManager.SetGameInProgress(0);
        PlayerPrefs.Save();
    }

}
