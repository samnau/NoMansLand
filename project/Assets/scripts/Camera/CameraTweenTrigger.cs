using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTweenTrigger : MonoBehaviour
{
    Camera mainCamera;
    Vector3 startPostion;
    Vector3 targetPosition;
    GameObject positionTrigger;
    GameObject positionTarget;
    GameObject player;
    BoxCollider2D triggerCollider;
    PositionTweener cameraTweener;
    bool tweenTriggered = false;
    bool triggerThrottled = false;
    [SerializeField]
    float tweenDuration = 2f;
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
        player = GameObject.FindGameObjectWithTag("Player");
        //positionTrigger = GameObject.FindGameObjectWithTag("CameraTrigger");
        positionTarget = GameObject.FindGameObjectWithTag("CameraTarget");
        mainCamera = Camera.main;
        startPostion = mainCamera.transform.position;
        targetPosition = positionTarget.transform.position;
        cameraTweener = mainCamera.GetComponent<PositionTweener>();
        //triggerCollider = positionTrigger.GetComponent<BoxCollider2D>();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("camera tween was triggered");
    //    ToggleCameraTween();
    //    //if (collision.CompareTag("Player"))
    //    //{
    //    //    print("camera tween was triggered");
    //    //    ToggleCameraTween();
    //    //}
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.isTrigger && !tweenTriggered)
        {
            if (mainCamera.GetComponent<Animator>() != null)
            {
                mainCamera.GetComponent<Animator>().enabled = false;
            }
            ToggleCameraTween();
            tweenTriggered = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && tweenTriggered)
        {
            //if(mainCamera.GetComponent<Animator>() != null)
            //{
            //    mainCamera.GetComponent<Animator>().enabled = true;
            //}
            //print(collision.GetComponent<BoxCollider2D>()?.size.y);
            tweenTriggered = false;
        }
    }

    IEnumerator ThrottleTrigger()
    {
        triggerThrottled = true;
        yield return new WaitForSeconds(tweenDuration * 2f);
        triggerThrottled = false;
    }

    void ToggleCameraTween()
    {
        Vector3 targetTweenPosition = tweenTriggered ? startPostion : targetPosition;
        cameraTweener.TriggerPositionByDuration(targetTweenPosition, tweenDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
