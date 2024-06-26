using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKingController : MonoBehaviour
{
    PositionTweener positionTweener;
    Vector3 startPosition;
    [SerializeField] float hoverModifier = .5f;
    [SerializeField] float hoverDuration = 3f;
    public bool activateHover = false;
    void Start()
    {
        positionTweener = this.GetComponent<PositionTweener>();
        startPosition = transform.position;
    }

    void IdleHover()
    {
        float targetPostionY = startPosition.y -= hoverModifier;
        Vector3 targetPosition = new Vector3(startPosition.x, targetPostionY, startPosition.z);
        positionTweener.StartYoYo(targetPosition, hoverDuration);
    }

    private void Update()
    {
        if(activateHover)
        {
            IdleHover();
            activateHover = false;
        }
    }

}
