using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrameSequencer : MonoBehaviour
{
    public List<GameObject> frames = new List<GameObject>();
    List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    [Range(0.0f, 1.0f)] public float speed = 0.1f;
    public bool activated = false;
    void Start()
    {
        if(frames.Count > 0)
        {
            foreach(GameObject frame in frames)
            {
                sprites.Add(frame.GetComponent<SpriteRenderer>());
            }

            if(activated)
            {
                TriggerFrameSequence();
            }
        }
    }

    public void TriggerFrameSequence()
    {
        StartCoroutine(SequenceFrames());
    }

    void ShowTargetSprite(int i = 0)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
        sprites[i].enabled = true;
    }

    IEnumerator SequenceFrames()
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            ShowTargetSprite(i);

            yield return new WaitForSeconds(speed);

            if (i == sprites.Count-1)
            {
                i = 0;
                ShowTargetSprite(i);

                yield return new WaitForSeconds(speed);
            }
        }
        yield return null;
    }
}
