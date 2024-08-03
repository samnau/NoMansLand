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

    public void Retreat()
    {
        Transform ghostWrapper = this.transform.parent;
        PositionTweener mainTweener = ghostWrapper.GetComponent<PositionTweener>();

        //Transform parentTransform = this.transform;
        Vector3 startPosition = ghostWrapper.position;
        Vector3 endPosition = new Vector3(startPosition.x - 3f, startPosition.y, startPosition.z);
        // yield return new WaitForSeconds(1f);

        //heroProfileAnimator.SetBool("ESCAPE", true);
        print($"mainTweener for ghost?: {mainTweener}");
        mainTweener.TriggerPositionByDuration(endPosition, 2.5f);

        //yield return new WaitForSeconds(.1f);

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
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
