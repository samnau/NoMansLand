using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Expression_Manager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public ExpressionAnimationManager expressionAnimationManager;

    public Text SpeakerText;
    string defaultName = "Molly";
    string defaultExpression = "idle";

    // Start is called before the first frame update
    void Start()
    {
        SpeakerText.text = defaultName;
    }
    public void Awake()
    {

        // Create a new command called 'camera_look', which looks at a target.
        dialogueRunner.AddCommandHandler(
            "SetSpeaker",     // the name of the command
            UpdateSpeaker // the method to run
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
