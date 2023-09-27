using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKingController : MonoBehaviour
{
    PositionTweener positionTweener;
    Vector3 startPosition;
    [SerializeField] float hoverModifier = .5f;
    [SerializeField] float hoverDuration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        positionTweener = this.GetComponent<PositionTweener>();
        startPosition = transform.position;
        IdleHover();
    }

    void IdleHover()
    {
        float targetPostionY = startPosition.y -= hoverModifier;
        Vector3 targetPosition = new Vector3(startPosition.x, targetPostionY, startPosition.z);
        positionTweener.StartYoYo(targetPosition, hoverDuration);
    }

}
