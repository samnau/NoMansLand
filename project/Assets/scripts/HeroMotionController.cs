using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroMotionController : MonoBehaviour
{
    Animator animator;
    public Animator downAnimator;
    Animator stateAnimator;
    public float motionDistance = 2.5f;
    private float runModifier = 2.5f;
    InputStateTracker inputStateTracker;
    Rigidbody2D myRigidBody2D;
    SpriteRenderer[] horizontalSprites = {};
    SpriteRenderer[] downSprites = {};
    SpriteRenderer[] upSprites = {};
    bool isHorizontalOnly = false;

    void Start()
    {
        stateAnimator = GetComponent<Animator>();
        var profileHero = GameObject.Find("hero-profile");
        animator = profileHero.GetComponent<Animator>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        inputStateTracker = GetComponent<InputStateTracker>();
        horizontalSprites = GetSpriteRenderers("hero-profile-state-wrapper");
        downSprites = GetSpriteRenderers("hero-front-wrapper");
        upSprites = GetSpriteRenderers("hero-up-wrapper");
    }
    private bool isMoving()
    {
        return inputStateTracker.isWalking;
    }

    SpriteRenderer[] GetSpriteRenderers(string parentName)
    {
        GameObject parentGameObject = gameObject.transform.Find(parentName)?.gameObject;
        return parentGameObject?.GetComponentsInChildren<SpriteRenderer>(true);
    }
    void ShowHorizontalSprites()
    {
        ToggleSprites(horizontalSprites, false);
        ToggleSprites(upSprites);
        ToggleSprites(downSprites);
    }
    void ShowUpSprites()
    {
        ToggleSprites(horizontalSprites);
        ToggleSprites(upSprites, false);
        ToggleSprites(downSprites);
    }
    void ShowDownSprites()
    {
        ToggleSprites(horizontalSprites);
        ToggleSprites(upSprites);
        ToggleSprites(downSprites, false);
    }
    void ToggleSprites(SpriteRenderer[] spriteArray, bool hideSprites = true)
    {
        float newAlpha = hideSprites ? 0f : 255f;
        foreach (SpriteRenderer sprite in spriteArray)
        {
            var newColor = sprite.color;
            newColor.a = newAlpha;
            sprite.color = newColor;
        }
    }
    void setAnimationStates()
    {
        animator.SetBool("WALK", isMoving());
        animator.SetBool("RUN", inputStateTracker.isRunning);
        downAnimator?.SetBool("WALK", isMoving());
        var currentDirection = inputStateTracker.direction;
        var isHorizontal = currentDirection == "left" || currentDirection == "right";
        var isDown = currentDirection == "down";
        var isUp = currentDirection == "up";

        isHorizontalOnly = isHorizontal && !isDown && !isUp;
        if(isHorizontalOnly)
        {
            ShowHorizontalSprites();
        } else if(isDown) {
            ShowDownSprites();
        } else if(isUp)
        {
            ShowUpSprites();
        }
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
            if (Input.GetAxis("Horizontal") != 0 && isHorizontalOnly)
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
