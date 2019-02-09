using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeTrigger : MonoBehaviour {

	public string sceneName;
	private GameObject sceneMangager;
	//IEnumerator ChangeLevel(){
		//float fadeTime = GameObject.Find ("SceneManager").GetComponent<FADE_CONTROLLER>().BeginFade (1);
		//yield return new WaitForSeconds (fadeTime);
		//SceneManager.LoadScene ("Test_Scene1");
		//yield return new WaitForSeconds (fadeTime);
		//GameObject.Find ("SceneManager").GetComponent<FADE_CONTROLLER>().BeginFade (-1);
	//}

	// Use this for initialization

	void OnTriggerEnter2D( Collider2D other ){
		if(other.CompareTag("Player")) {
			sceneMangager.GetComponent<Fade_Controller> ().triggerLevelChange (sceneName);
		}
	}

	void Start () {
		sceneMangager = GameObject.Find ("Scene_Manager");
		//GameObject.Find ("Scene_Manager").GetComponent<FADE_CONTROLLER> ().triggerLevelChange ("Forest2");
		//StartCoroutine (ChangeLevel ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
