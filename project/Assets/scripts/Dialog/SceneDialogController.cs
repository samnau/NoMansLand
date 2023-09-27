using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogController : MonoBehaviour
{
    GameObject targetSpeaker;
    GameObject previousSpeaker;
    GameObject heroAvatar;
    PositionTweener positionTweener;
    GameEvent switchSpeaker;
    [SerializeField] List<GameObject> speakerList;

    // PLANNING: listen for broadcast event and switch speaker on that event
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HideSpeaker(GameObject speaker)
    {

    }

    void ShowSpeaker(GameObject speaker)
    {

    }

    IEnumerator SwitchSpeaker()
    {
        HideSpeaker(previousSpeaker);
        ShowSpeaker(targetSpeaker);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
