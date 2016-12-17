using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeTrigger : MonoBehaviour {

	//IEnumerator ChangeLevel(){
		//float fadeTime = GameObject.Find ("SceneManager").GetComponent<FADE_CONTROLLER>().BeginFade (1);
		//yield return new WaitForSeconds (fadeTime);
		//SceneManager.LoadScene ("Test_Scene1");
		//yield return new WaitForSeconds (fadeTime);
		//GameObject.Find ("SceneManager").GetComponent<FADE_CONTROLLER>().BeginFade (-1);
	//}

	// Use this for initialization
	void Start () {
		GameObject.Find ("SceneManager").GetComponent<FADE_CONTROLLER> ().triggerLevelChange ("Forest4");
		//StartCoroutine (ChangeLevel ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
