using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : BaseButton
{

    protected override void ButtonInit()
    {
        base.ButtonInit();
        button.onClick.AddListener(() => Application.Quit());
    }

}
