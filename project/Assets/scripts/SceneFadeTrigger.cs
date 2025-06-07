using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeTrigger : MonoBehaviour {

	[SerializeField]
	string targetSceneName;
	Fade_Controller fadeController;
	enum Directions { up, down, left, right };
	[SerializeField] Directions direction;
	ScenePosition scenePosition;
	public bool disableTrigger = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player") && !disableTrigger) {
			TriggerSceneChange();
		}
	}

	void TriggerSceneChange()
	{
		scenePosition.currentDirection = scenePosition.startDirection[direction.ToString()];
		scenePosition.lastDirection = direction.ToString();
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
		fadeController = GameObject.FindObjectOfType<Fade_Controller>();
		scenePosition = fadeController.scenePosition;
	}
}
