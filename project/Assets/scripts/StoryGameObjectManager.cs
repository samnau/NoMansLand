// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryGameObjectManager : MonoBehaviour {
  public GameObject SPEAKER_1;
  public GameObject SPEAKER_2;
  public GameObject SPEAKER_3;
  public GameObject SPEAKER_4;
  	void Start () {
		StartCoroutine (HideSpeaker());

		//SPEAKER_1.GetComponent<Renderer> ().enabled = false;
    }
	IEnumerator HideSpeaker(){
		yield return new WaitForSeconds(3.00f);
		Debug.Log ("OFF");
		SPEAKER_1.GetComponent<Renderer> ().enabled = false;
		//return SPEAKER_1.GetComponent<Renderer> ().enabled = false;
	}
}