using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    Vector2 currentPosition;
    Vector2 targetPosition;
    public float speed = 0.25f;
    float moveIncrement = 0;
    Canvas targetCanvas;
    GameObject dialogBg;
    GameObject dialogAvatar;
    GameObject dialogText;
    GameObject speakerName;

    void Start()
    {
        targetCanvas = FindObjectOfType<Canvas>();
        dialogBg = GameObject.Find("DialogBg");
        dialogText = GameObject.Find("Dialog_Text");
        speakerName = GameObject.Find("Speaker_Name");

    }
    void findCurrentPosition()
    {
        currentPosition = transform.position;
    }

    void MoveDialog()
    {
        moveIncrement += (Time.deltaTime * speed);
        findCurrentPosition();
        if (moveIncrement < 1.0f)
        {
            transform.position = Vector2.Lerp(currentPosition, targetPosition, moveIncrement);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
