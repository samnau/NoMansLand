using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("Music");
        MusicController[] musicControllers = GameObject.FindObjectsOfType<MusicController>();
        AudioSource audioSource = this.GetComponent<AudioSource>();
        AudioClip clip = audioSource.clip;
        bool clipMatch = false;

        if(musicControllers.Length > 1)
        {
            clipMatch = musicControllers[0].GetComponent<AudioSource>().clip == musicControllers[1].GetComponent<AudioSource>().clip;
            if(clipMatch)
            {
                Destroy(this.gameObject);
            } else
            {
                GameObject musicToDestroy = musicControllers.FirstOrDefault(musicController => musicController.GetComponent<AudioSource>().clip != clip).gameObject;
                Destroy(musicToDestroy);
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
