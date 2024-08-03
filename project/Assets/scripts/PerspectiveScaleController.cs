using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveScaleController : MonoBehaviour
{
    GameObject player;
    float currentY;
    [Range(0.0f, 1.0f)]
    public float minScale = 0.4f;
    [Range(0.0f, 1.0f)]
    public float maxScale = 0.8f;
    float scaleFactor;
    [Range(0.0f, 20.0f)]
    public float scaleAreaHeight = 13f;
    void Start()
    {
        player = gameObject;

        //currentY = player.transform.position.y;
        scaleFactor = maxScale - minScale;
    }
    float CalculateScale()
    {
        currentY = player.transform.position.y + ((scaleAreaHeight / 2));
        float positionScale = currentY / scaleAreaHeight;
        float targetPositionScale = positionScale >= 0 ? positionScale : 0;
        float newPlayerScaleFactor = maxScale - (scaleFactor * targetPositionScale);
        return newPlayerScaleFactor;
    }
    void SetPlayerScale()
    {
        float newPlayerScaleFactor = CalculateScale();
        Vector3 newPlayerScale = new Vector3(newPlayerScaleFactor, newPlayerScaleFactor, 1);
        player.transform.localScale = newPlayerScale;
    }

    void Update()
    {
        SetPlayerScale();
    }
}
