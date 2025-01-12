using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    protected GameObject panelBG;
    [SerializeField]
    protected GameObject menuPanel;
    [SerializeField]
    protected Text titleText;
    [SerializeField]
    protected GameObject confirmationUI;
    [SerializeField]
    protected GameObject settingsUI;
    [SerializeField]
    protected GameObject pauseMenuUI;
    [SerializeField]
    GameState gameState;

    Button startButton;

    protected bool menuPanelActive = false;
    protected ColorTweener panelColorTweener;
    protected PositionTweener menuPanelPositionTweener;

    protected Vector3 onPosition = new Vector3(0, 0, 0);
    protected Vector3 offPosition = new Vector3(0, -245f, 0);
    protected Vector3 settingsOnPosition = new Vector3(0, 0, 0);
    protected Vector3 settingsOffPosition = new Vector3(0, -245f, 0);
    protected Vector3 confirmationOnPosition = new Vector3(0, -100f, 0);
    Color offColor;
    Color onColor;

    Button[] menuButtons;

    // Start is called before the first frame update
    void Start()
    {
        InitMenu();
    }

    protected virtual void InitMenu()
    {
        float menuPanelZPos = menuPanel.transform.position.z;
        onPosition = new Vector3(onPosition.x, onPosition.y, menuPanelZPos);
        offPosition = new Vector3(offPosition.x, offPosition.y, menuPanelZPos);
        panelColorTweener = panelBG?.GetComponent<ColorTweener>();
        menuPanelPositionTweener = menuPanel?.GetComponent<PositionTweener>();
        var defaultColor = panelBG.GetComponent<Image>().color;
        onColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.85f);
        offColor = new Color(onColor.r, onColor.g, onColor.b, 0);

        panelBG.transform.localPosition = new Vector3(0, 608f, 0);
        confirmationUI?.SetActive(false);
        pauseMenuUI?.SetActive(false);
        settingsUI?.SetActive(true);

        menuButtons = gameObject.GetComponentsInChildren<Button>();

        foreach (Button button in menuButtons)
        {
            if (button.GetComponent<StartButton>() != null)
            {
                startButton = button;
            }
        }
    }

    public IEnumerator ToggleMenuPanelSequence(Vector3 offPos, Vector3 onPos)
    {
        float duration = 0.5f;
        var targetPos = menuPanelActive ? offPos : onPos;
        var targetColor = menuPanelActive ? offColor : onColor;
        var panelBGOffPos = new Vector3(0, 608f, 0);
        var panelBGOnPos = new Vector3(0, 0, 0);
        if (!menuPanelActive)
        {
            panelBG.transform.localPosition = panelBGOnPos;
        }
        menuPanelPositionTweener.TriggerLocalPositionByDuration(targetPos, duration);
        panelColorTweener.TriggerImageColorByDuration(targetColor, duration);
        yield return new WaitForSeconds(duration);
        if (menuPanelActive)
        {
            panelBG.transform.localPosition = panelBGOffPos;
        }
        menuPanelActive = !menuPanelActive;

    }

    public void ToggleMenuPanel()
    {
        titleText.text = "Controls";
        bool settingsPanelActive = settingsUI.activeSelf;
        if(settingsPanelActive)
        {
            ToggleSettingsPanel();
        } else
        {
            ToggleConfirmationPanel();
        }

        startButton.Select();
    }

    protected void EnableDisableAllButtons(bool enabled = true)
    {
        foreach (Button button in menuButtons)
        {
            button.enabled = enabled;
        }
    }

    public void ToggleSettingsPanel()
    {
        confirmationUI?.SetActive(false);
        settingsUI?.SetActive(true);
        pauseMenuUI?.SetActive(false);
        titleText.text = "Controls";
        StartCoroutine(ToggleMenuPanelSequence(settingsOffPosition, settingsOnPosition));
    }

    public void ToggleConfirmationPanel()
    {
        confirmationUI?.SetActive(true);
        settingsUI?.SetActive(false);
        pauseMenuUI?.SetActive(false);
        titleText.text = "Are you sure?";
        StartCoroutine(ToggleMenuPanelSequence(offPosition, confirmationOnPosition));
    }

    public void EnableBonusButton()
    {
        if(gameState != null)
        {
            gameState.gameComplete = true;
        }
    }

}
