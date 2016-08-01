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
	string opposingSpeakerName;
	string currentTextString;
	string[] targetTextArray;
	string[] speakerArray ={"SPEAKER_1","SPEAKER_2","SPEAKER_3","SPEAKER_4"};
	string targetSpeaker  = "1";
	GameObject targetTextBox;
	GameObject storyManager;
	GameObject SPEAKER_1;
	GameObject currentSpeakerHead;
	GameObject opposingSpeakerHead;
	SpeakerMotionController SPEAKER_1_Controller;
	SpeakerMotionController currentSpeakerController;
	SpeakerMotionController opposingSpeakerController;
	StoryTextManager storyManagerController;
	StoryTextManager.Row targetStoryData;
	Text textComponent;
	// Use this for initialization
	void Start () {
		targetTextBox = GameObject.FindGameObjectWithTag ("TextBox_LEFT");
		storyManager = GameObject.FindGameObjectWithTag ("Story_Manager");
		storyManagerController = storyManager.GetComponent<StoryTextManager> ();
		SPEAKER_1 = GameObject.FindGameObjectWithTag ("SPEAKER_1");
		SPEAKER_1_Controller = SPEAKER_1.GetComponent<SpeakerMotionController> ();
		findStoryData(targetScene);
		parseTargetSpeakerText ();
		showSpeakerHead ();
		SPEAKER_1_Controller.ToggleMovement ();
		textComponent = targetTextBox.GetComponent<Text> ();
		StartCoroutine (TypeOutLines());
	}
	void showSpeakerHead (){
		toggleNonTargetSpeakers("SPEAKER_1");
		toggleNonTargetSpeakers("SPEAKER_2");
	}
	string findTargetSpeakerName(string speakerType){
		return targetStoryData.GetType ().GetField (speakerType).GetValue (targetStoryData) as string;
	}
	void toggleNonTargetSpeakers(string targetSpeaker){
		GameObject hiddenSpeaker;
		string hiddenSpeakerName;
		for (int y = 0; y < speakerArray.Length; y++) {
			if (speakerArray [y] != targetSpeaker) {
				hiddenSpeakerName = findTargetSpeakerName(speakerArray[y]);
				if(hiddenSpeakerName.Length > 0){
					hiddenSpeaker = GameObject.FindGameObjectWithTag (targetSpeaker).transform.Find (hiddenSpeakerName).gameObject;
					hiddenSpeaker.GetComponent<Renderer> ().enabled = false;
				}
			}
		}
	}
	void parseTargetSpeakerText (){
		string targetProperty = "TEXT_" + targetSpeaker;
		currentTextString = targetStoryData.GetType ().GetField (targetProperty).GetValue (targetStoryData) as string;
	}
	void findStoryData(string targetSceneName){
		targetStoryData = storyManagerController.Find_SCENENAME (targetSceneName);
	}
	//
	void setTargetSpeakerIndex (string speakerName){
		//var simpleName = speakerName.Substring (speakerName.IndexOf ("[") + 1, speakerName.IndexOf ("]") - 1);
		setCurrentSpeaker (speakerName);
		setOpposingSpeaker ();
		targetSpeaker = currentSpeakerName.Split ('_') [1];
	}
	IEnumerator TypeOutLines(){
		yield return new WaitForSeconds(0.6f);
		var textArray = currentTextString.Split('*');
		var startIndex = (speakerCounters [currentSpeakerName]);
		//switching to startIndex caused a freeze after speaker switching
		for (int z = startIndex; z < textArray.Length; z++)
		{
			IEnumerable TypeCoroutine = TypeLetters (textArray [z]);
			var isSwitchStatement = textArray [z].IndexOf("[") != -1;
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
	void setCurrentSpeaker(string speakerName){
		currentSpeakerName = speakerName.Substring (speakerName.IndexOf ("[") + 1, speakerName.IndexOf ("]") - 1);
		currentSpeakerHead = GameObject.FindGameObjectWithTag (currentSpeakerName);
		currentSpeakerController = currentSpeakerHead.GetComponent<SpeakerMotionController> ();
	}
	void setOpposingSpeaker(){
		opposingSpeakerName = currentSpeakerName == "SPEAKER_1" ? "SPEAKER_2" : "SPEAKER_1";
		opposingSpeakerHead = GameObject.FindGameObjectWithTag (opposingSpeakerName);
		opposingSpeakerController = opposingSpeakerHead.GetComponent<SpeakerMotionController> ();
	}
//	int findSpeakerIndex (string currentSpeakerName){
//		return currentSpeakerName.Split ('_') [1];
//	}
	IEnumerator SwitchSpeaker(string newSpeaker){
		setTargetSpeakerIndex (newSpeaker);
		currentSpeakerController.ToggleMovement ();
		opposingSpeakerController.ToggleMovement ();
		parseTargetSpeakerText ();
		yield return StartCoroutine (TypeOutLines());
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
