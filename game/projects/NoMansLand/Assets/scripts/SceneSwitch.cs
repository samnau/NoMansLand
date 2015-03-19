using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	void Start () {
		//Application.LoadLevel ("Scene2"); 
	}
	void OnTriggerEnter2D( Collider2D other ){
		if(other.CompareTag("hero")) {
				Debug.Log("Forest1 Up");
				Application.LoadLevel(sceneName);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
