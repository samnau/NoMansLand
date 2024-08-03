using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	void Start () {
		//Application.LoadLevel ("Scene2"); 
	}
	public void TriggerFadeIn (){
		GetComponent<Animator>().SetBool("fadeIn",true);
	}
	private void LoadNextScene() {
		//Application.LoadLevelAdditive(sceneName);
		SceneManager.LoadScene(sceneName);
	}
	void OnTriggerEnter2D( Collider2D other ){
//		if(other.CompareTag("hero")) {
//				//Debug.Log("Forest1 Up");
//				TriggerFadeIn()
//				//LoadNextScene();
//				//Application.LoadLevelAdditive(sceneName);
//				//Application.LoadLevel(sceneName);
//		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
