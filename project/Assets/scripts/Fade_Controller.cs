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
		//NOTE: no data loading needed here
	}

	public void SaveData(ref GlobalGameData data)
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		bool isMenuScene = currentSceneName.ToLower().Contains("menu");
		bool isIntroScene = currentSceneName.ToLower().Contains("forestintro");
		if (!isMenuScene)
        {
			data.worldState.currentScene = isIntroScene ? "Forest1" : currentSceneName;
		}
	}
}
