using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenuController : MainMenuController
{
    Vector3 pauseOnPosition = new Vector3(0, 56f, 0f);
    Vector3 pauseOffPosition = new Vector3(0, -215f, 0);
    [SerializeField]
    GameEvent openPauseMenu;
    [SerializeField]
    GameEvent closePauseMenu;
    Canvas mainCanvas;
    int defaultCanvasSortOrder;
    int activeCanvasSortOrder = 400;

    string defaultTutorialLayer;
    string hiddenTutorialLayer = "Cover";

    BattleTutorialManager tutorialManager;
    SortingGroup tutorialGroup;

    protected override void InitMenu()
    {
        base.InitMenu();
        mainCanvas = GetComponentInParent<Canvas>();
        defaultCanvasSortOrder = mainCanvas.sortingOrder;
        EnableDisableAllButtons(false);
        tutorialManager = mainCanvas.GetComponentInChildren<BattleTutorialManager>();
        tutorialGroup = tutorialManager?.gameObject.GetComponent<SortingGroup>();
        defaultTutorialLayer = tutorialGroup?.sortingLayerName;
    }

    public void TogglePausePanel()
    {
        StartCoroutine(ToggleTimeScale());

        confirmationUI?.SetActive(false);
        settingsUI?.SetActive(true);
        pauseMenuUI?.SetActive(true);
        titleText.text = "Game Paused";
        StartCoroutine(ToggleMenuPanelSequence(pauseOffPosition, pauseOnPosition));
        if(menuPanelActive)
        {
            mainCanvas.sortingOrder = defaultCanvasSortOrder;
            tutorialGroup.sortingLayerName = defaultTutorialLayer;
            EnableDisableAllButtons(false);
            closePauseMenu.Invoke();
        } else
        {
            EnableDisableAllButtons(true);
            mainCanvas.sortingOrder = activeCanvasSortOrder;
            tutorialGroup.sortingLayerName = hiddenTutorialLayer;
            openPauseMenu?.Invoke();
        }
    }

    IEnumerator ToggleTimeScale()
    {
        if(menuPanelActive)
        {
            Time.timeScale = 1f;
        }
        var targetTimeScale = menuPanelActive ? 1f : 0f;
        yield return new WaitForSeconds(.5f);
        if(!menuPanelActive)
        {
            Time.timeScale = 0f;
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
