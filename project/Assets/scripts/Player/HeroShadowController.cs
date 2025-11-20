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
        shadowPositions["battleidle"] = SetValues(-0.35f, 0.2f);
        shadowPositions["rightidle"] = SetValues(0.5f, 0);
        shadowPositions["rightwalk"] = SetValues(0.35f, 0);
        shadowPositions["downidle"] = SetValues(-0.2f, 0);
        shadowPositions["downwalk"] = SetValues(-0.2f, 0.1f);
        shadowPositions["upidle"] = SetValues(-0.2f, 0.2f);
        shadowPositions["upwalk"] = SetValues(-0.2f, 0);

        shadowScales["leftidle"] = SetValues(2.2f, 0.9f);
        shadowScales["leftwalk"] = SetValues(3.2f, 0.8f);
        shadowScales["battleidle"] = SetValues(3.2f, 1.1f);
        shadowScales["rightidle"] = SetValues(2.2f, 0.9f);
        shadowScales["rightwalk"] = SetValues(3.2f, 0.8f);
        shadowScales["downidle"] = SetValues(2.2f, 0.9f);
        shadowScales["downwalk"] = SetValues(2.2f, 1.2f);
        shadowScales["upidle"] = SetValues(2.2f, 0.9f);
        shadowScales["upwalk"] = SetValues(2.2f, 1.2f);

    }
    // REFACTOR: migrate this into a simplified function that calls the shared shadow transform method
    // leave as is for the demo
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

    void TriggerShadowTransform(string directionKeyName)
    {
        float zPos = 0;
        Tuple<float, float> newPositionItems = shadowPositions[directionKeyName];
        Vector3 newPosition = new Vector3(newPositionItems.Item1, newPositionItems.Item2, zPos);
        positionTweener?.TriggerLocalPositionByDuration(newPosition, speed);
        float zScale = transform.localScale.z;
        Tuple<float, float> newScaleItems = shadowScales[directionKeyName];
        Vector3 newScale = new Vector3(newScaleItems.Item1, newScaleItems.Item2, zScale);
        scaleTweener?.TriggerIrregularScaleByDuration(newScale, speed);
    }

    void ManualShadowTween(Vector3 pos, Vector3 scale, float duration = 0.5f)
    {
        positionTweener.TriggerLocalPositionByDuration(pos, duration);
        scaleTweener.TriggerIrregularScaleByDuration(scale, duration);
    }

    Vector3 CreateTweenVector(float xVal, float yVal, float zVal = 0f)
    {
        return new Vector3(xVal, yVal, zVal);
    }

    Vector3 CreateScaleVector(float xVal, float yVal)
    {
        return CreateTweenVector(xVal, yVal, transform.localScale.z);
    }

    public void TriggerFallSequence()
    {
        StartCoroutine(FallSequenceShadow());
    }

    IEnumerator FallSequenceShadow()
    {
        Vector3 startPos = CreateTweenVector(-.49f, .13f);
        var startScale = CreateScaleVector(0f, 0f);
        
        Vector3 landPos = CreateTweenVector(-.49f, .13f);
        var landScale = CreateScaleVector(3f, 1f);

        var fallPos = CreateTweenVector(1.4f, .13f);
        var fallScale = CreateScaleVector(6f, 1.75f);

        var kneelPos = CreateTweenVector(0.5f, .13f);
        var kneelScale = CreateScaleVector(3f, 1.5f);

        var standPos = CreateTweenVector(1.1f, .13f);
        var standScale = CreateScaleVector(2.2f, 1.2f);

        var backstepPos = CreateTweenVector(-.3f, .13f);
        // no change in scale

        var stumblePos = CreateTweenVector(0f, .13f);
        // no change in scale

        var forwardStepPos = CreateTweenVector(0.46f, .13f);
        var forwardStepScale = CreateScaleVector(1.987597f, 1.008615f);
        transform.localPosition = startPos;
        transform.localScale = startScale;

        float landTiming = .55f;
        //positionTweener.TriggerLocalPositionByDuration(landPos, landTiming);
        //scaleTweener.TriggerIrregularScaleByDuration(landScale, landTiming);
        ManualShadowTween(landPos, landScale, landTiming);
        yield return new WaitForSeconds(.5f);

        float fallTiming = .333333333f;
        ManualShadowTween(fallPos, fallScale, fallTiming);
        yield return new WaitForSeconds(fallTiming);
        yield return new WaitForSeconds(2.75f);
        float kneelTiming = .25f;
        ManualShadowTween(kneelPos, kneelScale, kneelTiming);
        yield return new WaitForSeconds(.75f);

        ManualShadowTween(standPos, standScale, .5f);
        yield return new WaitForSeconds(1.4f);

        ManualShadowTween(backstepPos, standScale, .75f);
        yield return new WaitForSeconds(.75f);

        ManualShadowTween(forwardStepPos, forwardStepScale, .4f);

        yield return null;
    }

    public void TargetedTransformShadow(string directionKeyName)
    {
        TriggerShadowTransform(directionKeyName);
    }

    public void TransformShadowByInput()
    {
        string currentDirection = inputStateTracker.direction;
        string directionState = inputStateTracker.isWalking ? "walk" : "idle";
        string directionKeyName = $"{currentDirection}{directionState}";
        TriggerShadowTransform(directionKeyName);
    }

    Tuple<float, float> SetValues(float xVal, float yVal)
    {
        return new Tuple<float, float>(xVal, yVal);
    }

}
