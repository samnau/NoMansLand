using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextImporter : MonoBehaviour {
	public TextAsset testText1;
	public TextAsset testText2;
	public string[] textLines;
	public string targetScene;
//	int speaker1Counter = 0;
//	int speaker2Counter = 0;
//	int speaker3Counter = 0;
//	int speaker4Counter = 0;
	Dictionary <string, int> speakerCounters = new Dictionary <string, int>{
		{ "SPEAKER_1", 0 },
		{ "SPEAKER_2", 0 },
		{ "SPEAKER_3", 0 },
		{ "SPEAKER_4", 0 }
	};
	string currentSpeakerName = "SPEAKER_1";
	string currentTextString;
	string[] targetTextArray;

	string targetSpeaker  = "1";
	GameObject targetTextBox;
	GameObject storyManager;
	StoryTextManager storyManagerController;
	StoryTextManager.Row targetStoryData;
	Text textComponent;
	// Use this for initialization
	void Start () {
		//targetScene = "intro";
		targetTextBox = GameObject.FindGameObjectWithTag ("TextBox_LEFT");
		storyManager = GameObject.FindGameObjectWithTag ("Story_Manager");
		storyManagerController = storyManager.GetComponent<StoryTextManager> ();

		findStoryData(targetScene);
		Debug.Log (targetStoryData.TEXT_1);
		parseTargetSpeakerText ();
		textComponent = targetTextBox.GetComponent<Text> ();
		StartCoroutine (TypeOutLines());
	}
	void parseTargetSpeakerText (){
		string targetProperty = "TEXT_" + targetSpeaker;
		Debug.Log (targetProperty);
		currentTextString = targetStoryData.GetType ().GetField (targetProperty).GetValue (targetStoryData) as string;
	}
	void findStoryData(string targetSceneName){
		targetStoryData = storyManagerController.Find_SCENENAME (targetSceneName);
	}
	void setTargetSpeakerIndex (string speakerName){
		//var simpleName = speakerName.Substring (speakerName.IndexOf ("[") + 1, speakerName.IndexOf ("]") - 1);
		setCurrentSpeakerName (speakerName);
		Debug.Log (currentSpeakerName);
		targetSpeaker = currentSpeakerName.Split ('_') [1];
	}
	IEnumerator TypeOutLines(){
		var textArray = currentTextString.Split('*');
		var startIndex = (speakerCounters [currentSpeakerName]);
		//switching to startIndex caused a freeze after speaker switching
		for (int z = startIndex; z < textArray.Length; z++)
		{
			IEnumerable TypeCoroutine = TypeLetters (textArray [z]);
			var isSwitchStatement = textArray [z].IndexOf("[") != -1;
			Debug.Log (isSwitchStatement);
			if(isSwitchStatement){
				updateSpeakerState (z);
				yield return StartCoroutine( SwitchSpeaker(textArray [z]) );
				yield break;
			}else{
				yield return StartCoroutine( TypeCoroutine.GetEnumerator() );
				yield return new WaitForSeconds(1.0f);
			}

		}
	}
	void updateSpeakerState (int currentIndex){
		speakerCounters [currentSpeakerName] = currentIndex + 1;
	}
	void setCurrentSpeakerName(string speakerName){
		currentSpeakerName = speakerName.Substring (speakerName.IndexOf ("[") + 1, speakerName.IndexOf ("]") - 1);
	}
//	int findSpeakerIndex (string currentSpeakerName){
//		return currentSpeakerName.Split ('_') [1];
//	}
	IEnumerator SwitchSpeaker(string newSpeaker){

		setTargetSpeakerIndex (newSpeaker);
		parseTargetSpeakerText ();
		Debug.Log (currentTextString);
		yield return StartCoroutine (TypeOutLines());
		//yield return new WaitForSeconds(1.0f);
	}
	IEnumerable TypeLetters(string textLine)
	{
		var textValue = textLine;
		for (int i = 0; i < textValue.Length+1; i++)
		{
			var currentText = textValue.Substring(0, i);
			textComponent.text = currentText;
			yield return new WaitForSeconds(0.05f);
		}
	}

//	// Update is called once per frame
//	void Update () {
//
//	}
}
