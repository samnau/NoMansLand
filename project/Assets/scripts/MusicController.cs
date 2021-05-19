using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("Music");

        if(musicPlayers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
