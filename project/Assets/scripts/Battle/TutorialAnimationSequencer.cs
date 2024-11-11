using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimationSequencer : MonoBehaviour
{
    ColorTweener[] colorTweeners;
    UtilityScaleTweener[] utilityScaleTweeners;
    [SerializeField]
    protected GameObject exclaimationPoint;
    [SerializeField]
    protected GameObject spaceBar;

    protected float targetSpaceBarScale;
    protected float targetExclaimationPointScale;

    public bool tutorialCompleted = false;

    BattleTutorialManager battleTutorialManager;
    // Start is called before the first frame update
    void Start()
    {
        InitTutorial();
    }

    private void OnEnable()
    {
        InitTutorial();
    }

    void InitTutorial()
    {
        battleTutorialManager = GetComponentInParent<BattleTutorialManager>();
        FindColorTweeners();
        FindScaleTweeners();

        targetSpaceBarScale = GetTargetTweenScale(spaceBar);
        targetExclaimationPointScale = GetTargetTweenScale(exclaimationPoint);
    }

    protected float GetTargetTweenScale(GameObject target)
    {
        float currentScale = target.transform.localScale.x;
        return currentScale * .9f;
    }

    public void ShowTutorial()
    {

        foreach (ColorTweener colorTweener in colorTweeners)
        {
            colorTweener.TriggerSpriteAlphaByDuration(1f, .5f);
        }

        StartTutorial();
    }

    protected virtual void StartTutorial()
    {
        print("start sequence method not implemented");
    }

    public virtual void StopTutorial()
    {
        foreach (UtilityScaleTweener utilityScaleTweener in utilityScaleTweeners)
        {
            utilityScaleTweener.scaleLooping = false;
        }

        foreach (ColorTweener colorTweener in colorTweeners)
        {
            colorTweener.TriggerSpriteAlphaByDuration(0f, .25f);
        }

        tutorialCompleted = true;
    }

    protected void FindColorTweeners()
    {
        colorTweeners = GetComponentsInChildren<ColorTweener>();
    }

    void FindScaleTweeners()
    {
        utilityScaleTweeners = GetComponentsInChildren<UtilityScaleTweener>();
    }

}
