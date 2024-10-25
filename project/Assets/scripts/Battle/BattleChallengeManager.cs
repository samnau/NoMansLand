using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChallengeManager : MonoBehaviour
{
    [SerializeField]
    GameEvent[] challengeEvents = new GameEvent[3];
    [SerializeField]
    GameObject[] challengeObjects = new GameObject[3];
    [HideInInspector]
    public int challengeIndex = 0;

    public void IncreaseChallengeIndex()
    {
        challengeIndex++;
        print($"the challenge index is {challengeIndex}");
    }
    public void TriggerChallengeEvent()
    {
        challengeEvents[challengeIndex].Invoke();
    }

    public void EnableTargetChallenge()
    {
        print($"the challenge will be: {challengeObjects[challengeIndex]?.name}");
        foreach(GameObject challengeObject in challengeObjects)
        {
            challengeObject.SetActive(false);
        }
        challengeObjects[challengeIndex].SetActive(true);
    }

}
