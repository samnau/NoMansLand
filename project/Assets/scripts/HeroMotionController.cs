using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMotionController : MonoBehaviour
{
    public GameObject Player;
    public Animator animator;
    public float motionDistance = 1.0f;
    InputStateTracker inputStateTracker;
    string[] directionValues = { "LEFT", "RIGHT", "UP", "DOWN" };

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        var profileHero = GameObject.Find("hero-profile");
        animator = profileHero.GetComponent<Animator>();
        inputStateTracker = GetComponent<InputStateTracker>();
    }
    private bool isMoving()
    {
        // moveKeyPressed = Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right");
        return inputStateTracker.isWalking;
    }
    void setAnimationStates()
    {
        animator.SetBool("WALK", isMoving());
        var currentDirection = inputStateTracker.direction.ToUpper();
        foreach (string value in directionValues)
        {
            //var directonMatch = value == currentDirection;
            //animator.SetBool(value, directonMatch);
        }
        // checkForAdditionalInput();
    }
    // Update is called once per frame
    void Update()
    {
        setAnimationStates();
    }
}
