using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Expression_Manager : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    public ExpressionAnimationManager expressionAnimationManager;

    public Text SpeakerText;
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

        dialogueRunner.AddCommandHandler(
            "SetSpeaker",  
            UpdateSpeaker
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

    public void UpdateSpeaker(string[] parameters)
    {
        if(parameters.Length <1)
        {
            SetSpeakerName(null);
            return;
        }
      var speakerName = parameters[0];
      var speakerEyesState = parameters.Length > 1 ? parameters[1] : "idle";
      var speakerMouthState = parameters.Length > 2 ? parameters[2] : "idle"; ;

      SetSpeakerName(speakerName);
      expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
    }
}
