using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("Music");
        MusicController[] musicControllers = FindObjectsOfType<MusicController>();
        AudioSource audioSource = this.GetComponent<AudioSource>();
        AudioClip clip = audioSource.clip;
        bool clipMatch = false;
        print(musicControllers.Length);

        if(musicControllers.Length > 1)
        {
            clipMatch = musicControllers[0].GetComponent<AudioSource>().clip.name == musicControllers[1].GetComponent<AudioSource>().clip.name;
            if(clipMatch)
            {
                print("clips match! destroy the old one!");

                Destroy(this.gameObject);
            } else
            {
                GameObject musicToDestroy = musicControllers.FirstOrDefault(musicController => musicController.GetComponent<AudioSource>().clip != clip).gameObject;
                Destroy(musicToDestroy);
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }
    public void FadeOutMusic()
    {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        AudioClip clip = audioSource.clip;
        StartCoroutine(StartFade(audioSource, 1f, 0));
    }

    public void SwitchMusic(AudioClip targetClip)
    {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = targetClip;
        audioSource.volume = 1f;
        audioSource.Play();
    }
}
