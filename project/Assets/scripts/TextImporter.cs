using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextImporter : MonoBehaviour {
	public TextAsset testText1;
	public TextAsset testText2;
	public string[] textLines;
	public string targetScene;
	int speaker1Counter = 0;
	int speaker2Counter = 0;
	int speaker3Counter = 0;
	int speaker4Counter = 0;
	string[] targetTextArray;
	int targetSpeaker  = 1;
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
		Debug.Log (targetStoryData.TEXT1);
		parseTargetSpeakerText ();
		textComponent = targetTextBox.GetComponent<Text> ();
		//StartCoroutine ()
		StartCoroutine (TypeOutLines(testText1.text));
	}
	void parseTargetSpeakerText (){
		string targetProperty = "TEXT" + targetSpeaker;

		Debug.Log (targetStoryData.GetType().GetField(targetProperty).GetValue(targetStoryData));
	}
	void findStoryData(string targetSceneName){
		targetStoryData = storyManagerController.Find_SCENENAME (targetSceneName);
	}

	IEnumerator TypeOutLines(string textLinesString){
		var textArray = textLinesString.Split('\n');
		for (int z = 0; z < textArray.Length; z++)
		{
			IEnumerable TypeCoroutine = TypeLetters (textArray [z]);
			yield return StartCoroutine( TypeCoroutine.GetEnumerator() );
			yield return new WaitForSeconds(1.0f);
		}
	}

	IEnumerable TypeLetters(string textLine)
	{
		var textValue = textLine;
		//Debug.Log(textValue);
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
