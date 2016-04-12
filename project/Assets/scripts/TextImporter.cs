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
		StartCoroutine ("TypeLetters");

		//textComponent.text = textLines [1];
	}

	public IEnumerator TypeLetters() 
	{
		var textValue = textLines [1];
		//Debug.Log (textValue);
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
