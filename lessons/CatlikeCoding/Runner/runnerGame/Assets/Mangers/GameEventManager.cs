using UnityEngine;
using System.Collections;

public class GameEventManager : MonoBehaviour {
	public delegate void GameEvent();

	public static event GameEvent GameStart, GameOver;

	public static void TriggerGameStart(){
		if(GameStart != null){
			GameStart();
		}
	}

	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
