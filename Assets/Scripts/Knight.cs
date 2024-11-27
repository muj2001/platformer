using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class Knight : MonoBehaviour
{   
    public Damageable damageable;
    public float walkSpeed = 5f;
    public DetectionZone attackZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    public Rigidbody2D playerRb;


    public enum WalkableDirection 
    {
        Right,
        Left
    }


    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection 
    {
        get
        {
            return _walkDirection;
        }
        private set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);    
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public float walkStopRate = 0.6f;

    public bool HasTarget { get {
        return _hasTarget;
    } private set {
        _hasTarget = value;
        animator.SetBool(AnimationStrings.hasTarget, value);
    } }

    public bool CanMove { get {
        return animator.GetBool(AnimationStrings.canMove);
    }
    }

    public bool IsAlive { get {
        return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            Debug.Log("Invalid WalkDirection: " + WalkDirection);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        damageable = GetComponent<Damageable>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {   
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (CanMove) {
            rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
        } else {
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        // Debug.Log(rb.position.x - playerRb.position.x); 
        if (IsAlive && HasTarget)
        {
            if (rb.position.x - playerRb.position.x > 0)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else if (rb.position.x - playerRb.position.x < 0)
            {
                WalkDirection = WalkableDirection.Right;
            }
        }
    }
}
