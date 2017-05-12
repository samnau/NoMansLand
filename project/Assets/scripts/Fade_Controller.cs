using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FADE_CONTROLLER : MonoBehaviour {
	//	public delegate void TriggerLevelChange();
	//	public static event TriggerLevelChange levelHasChanged;

	public Texture2D Overlay;
	public float fadeSpeed = 0.8f;
	public int drawDepth = -1000;
	float alpha = 1.0f;
	float fadeDirection = -1.0f;
	string sceneName;
	// Use this for initialization
	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		BeginFade (-1);
	}

	IEnumerator loadLevel (string sceneName){
		BeginFade (1);
		yield return new WaitForSeconds (fadeSpeed);
		SceneManager.LoadScene (sceneName);
	}
	public void triggerLevelChange(string sceneName){
		StartCoroutine (loadLevel (sceneName));
	}
	void OnGUI(){
		alpha += fadeSpeed * fadeDirection * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Overlay);
	}
	public float BeginFade (int direction){
		fadeDirection = direction;
		return (fadeSpeed);
	}
	void OnLevelWasLoaded(){
		BeginFade (-1);
	}
	// Update is called once per frame
	void Update () {

	}
	void Start(){
		BeginFade (-1);
	}
}
