using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject panelBG;
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    Text titleText;
    [SerializeField]
    GameObject confirmationUI;
    [SerializeField]
    GameObject settingsUI;

    Button startButton;

    bool menuPanelActive = false;
    ColorTweener panelColorTweener;
    PositionTweener menuPanelPositionTweener;

    Vector3 onPosition = new Vector3(0, 0, 0);
    Vector3 offPosition = new Vector3(0, -245f, 0);
    Vector3 settingsOnPosition = new Vector3(0, 0, 0);
    Vector3 settingsOffPosition = new Vector3(0, -245f, 0);
    Vector3 confirmationOnPosition = new Vector3(0, -100f, 0);
    Color offColor;
    Color onColor;


    Dictionary<string, Tuple<float, float>> panelPositions = new Dictionary<string, Tuple<float, float>>();

    // Start is called before the first frame update
    void Start()
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
        confirmationUI.SetActive(false);
        settingsUI.SetActive(true);

        foreach(Button button in gameObject.GetComponentsInChildren<Button>())
        {
            if(button.GetComponent<StartButton>() != null)
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

    public void ToggleSettingsPanel()
    {
        confirmationUI.SetActive(false);
        settingsUI.SetActive(true);
        titleText.text = "Controls";
        StartCoroutine(ToggleMenuPanelSequence(settingsOffPosition, settingsOnPosition));
    }

    public void ToggleConfirmationPanel()
    {
        confirmationUI.SetActive(true);
        settingsUI.SetActive(false);
        titleText.text = "Are you sure?";
        StartCoroutine(ToggleMenuPanelSequence(offPosition, confirmationOnPosition));
    }

}
