using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMatcher : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    Transform currentTransform;
    Vector3 targetPosition;
    Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentTransform = this.transform;
        targetPosition = targetTransform.position;
        currentPosition = currentTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = targetPosition;
    }
}
