using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMotionController : MonoBehaviour
{
    public GameObject Player;
    public Animator animator;
    public float motionDistance = 1.0f;
    InputStateTracker inputStateTracker;
    Rigidbody2D myRigidBody2D;
    string[] directionValues = { "LEFT", "RIGHT", "UP", "DOWN" };

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        var profileHero = GameObject.Find("hero-profile");
        animator = profileHero.GetComponent<Animator>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
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

    private void startMovement()
    {
        var horizontalValue = Input.GetAxis("Horizontal") * motionDistance;
        var verticalValue = Input.GetAxis("Vertical") * motionDistance;
        var directionModifier = horizontalValue < 0 ? 1 : -1;
        var walkingVelocityReached = Mathf.Abs(horizontalValue) > 0.5 || Mathf.Abs(verticalValue) > 0.5;

        if (walkingVelocityReached)
        {
            //myRigidBody2D.velocity = new Vector2(horizontalValue,myRigidBody2D.velocity.y);
            myRigidBody2D.velocity = new Vector2(horizontalValue, verticalValue);
            var myTransform = gameObject.transform;
            //var scaleX = myTransform.localScale.x;
            //var scaleY = myTransform.localScale.y;
            //var newScale = scaleX * directionModifier;
            var newRotation = horizontalValue < 0 ? 0f : 180f;
            //var scaleVector = new Vector3 (newScale, 1, 1);
            //print(newScale);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, newRotation, 0f));
           // myTransform.localScale = scaleVector;
        }
    }
    private void stopMovement()
    {
        myRigidBody2D.velocity = Vector2.zero;
    }
    void updateMovement()
    {
        if (!isMoving())
        {
            stopMovement();
            return;
        }
        startMovement();
    }
    // Update is called once per frame
    void Update()
    {
        setAnimationStates();
        updateMovement();
    }
}
