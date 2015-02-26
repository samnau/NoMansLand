using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public GUIText distanceText, boostsText, gameOverText, instructionsText, runnerText;
	// Use this for initialization
	private static GUIManager instance;

	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}
	
	public static void SetBoosts (int boosts){
		instance.boostsText.text = boosts.ToString();
	}

	public static void SetDistance(float distance){
		instance.distanceText.text = distance.ToString("f0");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			GameEventManager.TriggerGameStart();
		}
	}

	private void GameOver(){
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}

	private void GameStart ()
	{
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
	}
}