using UnityEngine;
using System.Collections;

public class KittyCreator : MonoBehaviour {
	public float minSpawnTime = 0.75f;
	public float maxSpawnTime = 2.0f;
	public GameObject catPrefab;
	// Use this for initialization
	void Start () {
		Invoke ("SpawnCat", minSpawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void SpawnCat(){
		Camera camera = Camera.main;
		float orthoSize = camera.orthographicSize;
		float camApsepct = camera.aspect;
		Vector3 cameraPos = camera.transform.position;
		float xMax = camApsepct * orthoSize;
		float xRange = xMax * 1.75f;
		float yMax = orthoSize - 0.5f;

		float randomXRange = Random.Range (xMax - xRange, xMax);
		float randomYRange = Random.Range (-yMax, yMax);
		Vector3 catPos = new Vector3 (cameraPos.x + randomXRange, randomYRange, catPrefab.transform.position.z);

		Instantiate (catPrefab, catPos, Quaternion.identity);
		Invoke("SpawnCat", Random.Range(minSpawnTime, maxSpawnTime));
	}
}
