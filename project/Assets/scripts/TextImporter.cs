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
		textComponent.text = textLines [0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
