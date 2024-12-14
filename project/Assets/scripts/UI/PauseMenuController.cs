using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MainMenuController
{
    Vector3 pauseOnPosition = new Vector3(0, 56f, 0f);
    Vector3 pauseOffPosition = new Vector3(0, -215f, 0);
    [SerializeField]
    GameEvent openPauseMenu;
    [SerializeField]
    GameEvent closePauseMenu;

    protected override void InitMenu()
    {
        base.InitMenu();
    }

    public void TogglePausePanel()
    {
        confirmationUI?.SetActive(false);
        settingsUI?.SetActive(true);
        pauseMenuUI?.SetActive(true);
        titleText.text = "Game Paused";
        StartCoroutine(ToggleMenuPanelSequence(pauseOffPosition, pauseOnPosition));
        if(menuPanelActive)
        {
            closePauseMenu.Invoke();
        } else
        {
            openPauseMenu?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }
}
