using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeTrigger : MonoBehaviour {

	public Object targetScene;
	Fade_Controller fadeController;
	enum Directions { up, down, left, right };
	[SerializeField] Directions direction;
	ScenePosition scenePosition;

	void OnTriggerEnter2D( Collider2D other ){
		if(other.CompareTag("Player")) {
		scenePosition.currentDirection = scenePosition.startDirection[direction.ToString()];
		scenePosition.lastDirection = direction.ToString();
		fadeController.triggerLevelChange(targetScene.name);
		}
	}

	void Start () {
		fadeController = GameObject.FindObjectOfType<Fade_Controller>();
		scenePosition = fadeController.scenePosition;
	}
}
