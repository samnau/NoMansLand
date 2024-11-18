using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTweenTrigger : MonoBehaviour
{
    Camera mainCamera;
    Vector3 startPostion;
    Vector3 targetPosition;
    GameObject positionTarget;
    PositionTweener cameraTweener;
    bool moveToTarget = true;
    bool startPosSet = false;
    [SerializeField]
    float tweenDuration = 1.5f;
    void Start()
    {
        InitCameraTrigger();
    }

    private void OnEnable()
    {
        InitCameraTrigger();
    }

    void InitCameraTrigger()
    {
        positionTarget = GameObject.FindGameObjectWithTag("CameraTarget");
        mainCamera = Camera.main;
        targetPosition = positionTarget.transform.position;
        cameraTweener = mainCamera.GetComponent<PositionTweener>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            if (mainCamera.GetComponent<Animator>() != null)
            {
                mainCamera.GetComponent<Animator>().enabled = false;
            }

            ToggleCameraTween();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            ToggleCameraTween();
        }
    }

    void ToggleCameraTween()
    {
        cameraTweener.StopAllCoroutines();
        if (!startPosSet)
        {
            var mainPos = mainCamera.transform.position;
            startPostion = mainPos;
            startPosSet = true;
        }
        
        Vector3 targetTweenPosition = moveToTarget ? targetPosition : startPostion;
        cameraTweener.TriggerPositionByDuration(targetTweenPosition, tweenDuration);
        moveToTarget = !moveToTarget;
    }

}
