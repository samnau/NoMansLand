using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeTrigger : MonoBehaviour, IGlobalDataPersistence {

	[SerializeField]
	string targetSceneName;
	Fade_Controller fadeController;
	enum Directions { up, down, left, right };
	[SerializeField] Directions direction;
	ScenePosition scenePosition;
	public bool disableTrigger = false;

	GlobalGameData loadedData;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player") && !disableTrigger) {
			TriggerSceneChange();
		}
	}

	void TriggerSceneChange()
	{
		scenePosition.currentDirection = scenePosition.startDirection[direction.ToString()];
		scenePosition.lastDirection = direction.ToString();
		// This file should not try to trigger data saves, only implment what happens. The manager will do the saving
		//SaveData(ref loadedData);
		if (targetSceneName != null)
		{
			fadeController.triggerLevelChange(targetSceneName);
		} else
		{
			Debug.Log("no target scene set");
		}
	}

	public void DisableTrigger()
	{
		disableTrigger = true;
	}

	public void EnableTrigger()
    {
		disableTrigger = false;
    }

	void Start () {
		fadeController = FindObjectOfType<Fade_Controller>();
		// COMEBACK: scene position code may be obsolte after the player prefs system was added.
		scenePosition = fadeController.scenePosition;
	}

	public void LoadData(GlobalGameData data)
	{
		//NOTE: no data loading needed here - method required by inferface
	}

	public void SaveData(ref GlobalGameData data)
	{
		data.worldState.lastDirection = this.direction.ToString();
	}
}
