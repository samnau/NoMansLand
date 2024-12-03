using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : BaseButton
{
    [SerializeField]
    GameEvent showSettings;

    protected override void ButtonInit()
    {
        base.ButtonInit();
        button.onClick.AddListener(TriggerShowSettings);
    }

    void TriggerShowSettings()
    {
        print("show the serttings screen!");
        showSettings?.Invoke();
    }
}
