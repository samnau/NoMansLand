using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroShadowController : MonoBehaviour
{
    Dictionary<string, Tuple<float, float>> shadowPositions = new Dictionary<string, Tuple<float, float>>();
    Dictionary<string, Tuple<float, float>> shadowScales = new Dictionary<string, Tuple<float, float>>();

    InputStateTracker inputStateTracker;
    Tuple<float, float> coordinates;
    UtilityScaleTweener scaleTweener;
    PositionTweener positionTweener;
    List<string> stateValues = new List<string> { "Idle", "Walk" };
    public string[] directionValues;
    float speed = 0.1f;
    GameObject shadow;
    bool canTween = true;
    // Start is called before the first frame update
    void Start()
    {
        shadow = this.gameObject;
        inputStateTracker = GetComponentInParent<InputStateTracker>();
        directionValues = inputStateTracker.directionValues;
        positionTweener = GetComponent<PositionTweener>();
        scaleTweener = GetComponent<UtilityScaleTweener>();
        foreach(string stateValue in stateValues)
        {
            foreach(string direction in directionValues)
            {
                shadowPositions.Add($"{direction}{stateValue}", Tuple.Create<float,float>(0,0));
                shadowScales.Add($"{direction}{stateValue}", Tuple.Create<float, float>(0, 0));
            }
        }

        shadowPositions["leftidle"] = SetValues(-0.5f, 0);
        shadowPositions["leftwalk"] = SetValues(-0.35f, 0);
        shadowPositions["rightidle"] = SetValues(0.5f, 0);
        shadowPositions["rightwalk"] = SetValues(0.35f, 0);
        shadowPositions["downidle"] = SetValues(-0.2f, 0);
        shadowPositions["downwalk"] = SetValues(-0.2f, 0.1f);
        shadowPositions["upidle"] = SetValues(-0.2f, 0.2f);
        shadowPositions["upwalk"] = SetValues(-0.2f, 0);

        shadowScales["leftidle"] = SetValues(2.2f, 0.9f);
        shadowScales["leftwalk"] = SetValues(3.2f, 0.8f);
        shadowScales["rightidle"] = SetValues(2.2f, 0.9f);
        shadowScales["rightwalk"] = SetValues(3.2f, 0.8f);
        shadowScales["downidle"] = SetValues(2.2f, 0.9f);
        shadowScales["downwalk"] = SetValues(2.2f, 1.2f);
        shadowScales["upidle"] = SetValues(2.2f, 0.9f);
        shadowScales["upwalk"] = SetValues(2.2f, 1.2f);

    }

    public void TransformShadow()
    {
        string currentDirection = inputStateTracker.direction;
        string directionState = inputStateTracker.isWalking ? "walk" : "idle";
        string directionKeyName = $"{currentDirection}{directionState}";
        float zPos = 0;
        Tuple<float, float> newPositionItems = shadowPositions[directionKeyName];
        Vector3 newPosition = new Vector3(newPositionItems.Item1, newPositionItems.Item2, zPos);
        positionTweener?.TriggerLocalPositionByDuration(newPosition, speed);
        float zScale = transform.localScale.z;
        Tuple<float, float> newScaleItems = shadowScales[directionKeyName];
        Vector3 newScale = new Vector3(newScaleItems.Item1, newScaleItems.Item2, zScale);
        scaleTweener?.TriggerIrregularScaleByDuration(newScale, speed);
    }

    Tuple<float, float> SetValues(float xVal, float yVal)
    {
        return new Tuple<float, float>(xVal, yVal);
    }

}
