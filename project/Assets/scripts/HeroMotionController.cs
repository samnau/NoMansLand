using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMotionController : MonoBehaviour
{
    public GameObject Player;
    public Animator animator;
    public Animator downAnimator;
    public Animator stateAnimator;
    public float motionDistance = 2.5f;
    private float runModifier = 2.5f;
    InputStateTracker inputStateTracker;
    Rigidbody2D myRigidBody2D;
    string[] directionValues = { "LEFT", "RIGHT", "UP", "DOWN" };

    // Start is called before the first frame update
    void Start()
    {
        stateAnimator = GetComponent<Animator>();
        var profileHero = GameObject.Find("hero-profile");
        //var downHero = GameObject.Find("hero-art-down");
        animator = profileHero.GetComponent<Animator>();
        //downAnimator = downHero.GetComponent<Animator>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        inputStateTracker = GetComponent<InputStateTracker>();
    }
    private bool isMoving()
    {
        return inputStateTracker.isWalking;
    }
    void setAnimationStates()
    {
        animator.SetBool("WALK", isMoving());
        animator.SetBool("RUN", inputStateTracker.isRunning);
        downAnimator.SetBool("WALK", isMoving());
        var currentDirection = inputStateTracker.direction.ToUpper();
        var isHorizontal = currentDirection == "LEFT" || currentDirection == "RIGHT";
        var isDown = currentDirection == "DOWN";
        stateAnimator.SetBool("HORIZONTAL", isHorizontal && !isDown);
        stateAnimator.SetBool("DOWN", isDown);
        // checkForAdditionalInput();
    }

    private void startMovement()
    {
        float targetRunModifier = inputStateTracker.isRunning ? runModifier : 0f;
        float motionSpeed = motionDistance + targetRunModifier;
        var horizontalValue = Input.GetAxis("Horizontal") * motionSpeed;
        var verticalValue = Input.GetAxis("Vertical") * motionSpeed;
        var walkingVelocityReached = Mathf.Abs(horizontalValue) > 0.5 || Mathf.Abs(verticalValue) > 0.5;

        if (walkingVelocityReached)
        {
            myRigidBody2D.velocity = new Vector2(horizontalValue, verticalValue);
            var newRotation = horizontalValue < 0 ? 0f : 180f;

            if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, newRotation, 0f));
            } else
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
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
