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

	// DOCS: this file allows the direction of the player from a transition to be passed and tracked
	// it also passes a target scene name to the Fade Controller to trigger that transition
	// and passes its direction to the fade controller for tracking and saving

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player") && !disableTrigger) {
			TriggerSceneChange();
		}
	}

	void TriggerSceneChange()
	{
		// NOTE: pass the direction value to the singular fade controller for save state
		fadeController.lastDirection = direction.ToString();

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
	}

}
