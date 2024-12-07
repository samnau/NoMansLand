using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject panelBG;
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    Text titleText;

    bool menuPanelActive = false;
    ColorTweener panelColorTweener;
    PositionTweener menuPanelPositionTweener;

    Vector3 onPosition = new Vector3(0, 0, 0);
    Vector3 offPosition = new Vector3(0, -245f, 0);
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
    }

    public IEnumerator ToggleMenuPanelSequence()
    {
        float duration = 0.5f;
        var targetPos = menuPanelActive ? offPosition : onPosition;
        var targetColor = menuPanelActive ? offColor : onColor;
        var panelBGOffPos = new Vector3(0, 608f, 0);
        var panelBGOnPos = new Vector3(0, 0, 0);
        if(!menuPanelActive)
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
        StartCoroutine(ToggleMenuPanelSequence());
    }

}
