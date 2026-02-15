using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class Expression_Manager : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    public ExpressionAnimationManager expressionAnimationManager;

    public TextMeshProUGUI SpeakerText;
    string defaultName = "Molly";
    string defaultExpression = "idle";

    void Start()
    {
        SpeakerText.text = defaultName;
        expressionAnimationManager.ChangeExpression(defaultExpression, "smile");
    }
    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        dialogueRunner.AddCommandHandler<string, string>(
            "SetExpression",
            SetExpression
        );
    }

    public void SetExpression(string eyeState, string mouthState)
    {
        var speakerEyesState = eyeState is null ? "idle" : eyeState;
        var speakerMouthState = mouthState is null ? "idle" : mouthState;

        expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
    }

}
