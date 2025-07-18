using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fade_Controller : MonoBehaviour, IGlobalDataPersistence {

	public Texture2D Overlay;
	public float fadeSpeed = 0.8f;
	public int drawDepth = -1000;
	float alpha = 1.0f;
	private float fadeDirection = -1.0f;
	readonly string sceneName;
	string targetSceneName;
	public ScenePosition scenePosition;
	public string lastDirection;

	// DOCS: this file mainly just accepts a scene name and triggers a scene transition. It does not track player direction values.
	// this file also tracks last direction so that a singular direction value can be saved
	// the scene fade triggers pass their direction to the fade controller to set the last direction

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		BeginFade(-1);
	}

	IEnumerator loadLevel (string sceneName, float delay = 0f){
		yield return new WaitForSeconds(delay);
		BeginFade (1);
		yield return new WaitForSeconds (fadeSpeed);
		targetSceneName = sceneName;
		SceneManager.LoadScene (sceneName);
	}
	public void triggerLevelChange(string sceneName){
		StartCoroutine(loadLevel (sceneName));
	}

	public void LevelChangeTimedTrigger(string sceneName, float delay)
    {
		StartCoroutine(loadLevel(sceneName, delay));
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

	void Start(){
		BeginFade (-1);
	}

	public void LoadData(GlobalGameData data)
	{
		lastDirection = data.worldState.lastDirection;
	}

	public void SaveData(ref GlobalGameData data)
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		bool isMenuScene = currentSceneName.ToLower().Contains("menu");
		bool isIntroScene = currentSceneName.ToLower().Contains("forestintro");
		data.worldState.lastDirection = lastDirection;
		if (!isMenuScene)
        {
			data.worldState.currentScene = isIntroScene ? "Forest1" : currentSceneName;
		}
	}
}
