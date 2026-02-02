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

        dialogueRunner.AddCommandHandler<string, string, string>(
            "SetSpeaker",  
            UpdateSpeaker
        );

        dialogueRunner.AddCommandHandler<string, string>(
            "SetExpression",
            SetExpression
        );
    }

    void SetSpeakerName(string name)
    {
        Debug.Log(name);
        if (name == null)
        {
            SpeakerText.text = defaultName;
            return;
        }
        SpeakerText.text = name;
    }

    public void SetExpression(string eyeState, string mouthState)
    {
        var speakerEyesState = eyeState is null ? "idle" : eyeState;
        var speakerMouthState = mouthState is null ? "idle" : mouthState;

        expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
    }
    //public void SetExpression(string[] parameters)
    //{
    //    var speakerEyesState = parameters.Length > 0 ? parameters[0] : "idle";
    //    var speakerMouthState = parameters.Length > 1 ? parameters[1] : "idle"; ;

    //    expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
    //}

    public void UpdateSpeaker(string speakerName, string eyeState, string mouthState)
    {
        if (speakerName is null )
        {
            SetSpeakerName(null);
            return;
        }
        //var speakerName = parameters[0];
        var speakerEyesState = eyeState is null ? "idle" : eyeState;
        var speakerMouthState = mouthState is null ? "idle" : mouthState;

        SetSpeakerName(speakerName);
        expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
    }

    //public void UpdateSpeaker(string[] parameters)
    //{
    //    if(parameters.Length <1)
    //    {
    //        SetSpeakerName(null);
    //        return;
    //    }
    //  var speakerName = parameters[0];
    //  var speakerEyesState = parameters.Length > 1 ? parameters[1] : "idle";
    //  var speakerMouthState = parameters.Length > 2 ? parameters[2] : "idle"; ;

    //  SetSpeakerName(speakerName);
    //  expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
    //}
}
