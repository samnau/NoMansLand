using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextImporter : MonoBehaviour {
	public TextAsset testText1;
	public TextAsset testText2;
	public string[] textLines;
	GameObject targetTextBox;
	Text textComponent;
	// Use this for initialization
	void Start () {
		targetTextBox = GameObject.FindGameObjectWithTag ("TextBox_LEFT");
		textComponent = targetTextBox.GetComponent<Text> ();
		if (testText1) {
			textLines = testText1.text.Split ('\n');
		} else if (testText2) {
			textLines = testText2.text.Split ('\n');
		}
		StartCoroutine (TypeOutLines(testText1.text));

		//textComponent.text = textLines [1];
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
		//Debug.Log (textValue);
		//var textArray = textLinesString.Split('\n');
		//print(textArray);
		for (int i = 0; i < textValue.Length; i++)
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
