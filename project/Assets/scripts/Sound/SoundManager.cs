using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void OnEnable()
    {
        Gem.OnGemCollected += PlayGemSound;
    }

    private void OnDisable()
    {
        Gem.OnGemCollected -= PlayGemSound;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayGemSound(ItemData itemData)
    {
        print("shiny gem noise");
    }
}
