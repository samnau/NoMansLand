using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTutorialManager : MonoBehaviour
{
    [SerializeField]
    Animator dialogAnimator;
    [SerializeField]
    Text dialogText;
    Color defaultTextColor;
    Color hiddenColor;
    [SerializeField]
    GameObject[] tutorials = new GameObject[3];
    [SerializeField]
    TutorialAnimationSequencer[] animationSequencers = new TutorialAnimationSequencer[3];
    public int tutorialIndex = 0;
    public int completedIndex = 0;
    public bool tutorialCompleted = false;
    //GameObject targetTutorial;
    // Start is called before the first frame update
    void Start()
    {
        defaultTextColor = dialogText.color;
        hiddenColor = new Color(defaultTextColor.r, defaultTextColor.g, defaultTextColor.b, 0f);
    }

    void TriggerDialogSystem()
    {
        ToggleDialogText();
        //dialogAnimator.SetBool("show", true);
    }
    public void EnableTargetTutorial()
    {
        ToggleDialogText();
        foreach (GameObject tutorialObject in tutorials)
        {
            tutorialObject.SetActive(false);
        }
        // this might not be used
        bool tutorialCompleted = GetTargetTutorialCompletedState();

        if(!tutorialCompleted)
        {
            animationSequencers[tutorialIndex].gameObject.SetActive(true);
            animationSequencers[tutorialIndex].ShowTutorial();
        }
    }

    public void HideTutorial()
    {
        var currentTutorial = animationSequencers[tutorialIndex];
        currentTutorial.StopTutorial();

        IncreaseTutorialIndex();
        ToggleDialogText();
    }

    void ToggleDialogText()
    {
        Color targetColor = dialogText.color.a < 1f ? defaultTextColor : hiddenColor;
        dialogText.color = targetColor;
    }

    public bool GetTargetTutorialCompletedState()
    {
        return animationSequencers[tutorialIndex].tutorialCompleted;
    }

    public void ShowCurrentTutorial()
    {
        GameObject targetTutorial = tutorials[tutorialIndex];
        // placeholder: targetTutorial.playIntro
    }

    public void IncreaseTutorialIndex()
    {
        tutorialIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
