using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D theCollision) {
		print (theCollision);
		if(theCollision.gameObject.tag=="Player") {
			Application.LoadLevel (sceneName); 
		}
		
	}



}
