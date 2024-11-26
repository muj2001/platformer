using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpImpulse = 5f;
    
    // TODO: Change Air Speed according to the speed when Grounded
    public float airSpeed = 3f;
    Vector2 moveInput;
    Rigidbody2D rb;
    private Animator animator;
    TouchingDirections touchingDirections;

    [SerializeField] private bool _isMoving = false;
    public bool IsMoving { get 
        { 
            return _isMoving;
        } 
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    public float CurrentMoveSpeed { get
        {   if (CanMove)
            {  
                if(IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded && !IsCrouch)
                    {
                        if(IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    } 
                    else if (touchingDirections.IsGrounded && IsCrouch)
                    {
                        return walkSpeed / 2;
                    }
                    else 
                    {
                        return airSpeed;
                    }
                } 
                else 
                {
                    // If the player is not moving, the speed is 0
                    return 0;
                }
            } else {
                // Movement Locked
                return 0;
            }
        }
    }

    [SerializeField] private bool _isRunning = false;
    public bool IsRunning { get 
        { 
            return _isRunning;
        } 
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        } 
    }

    [SerializeField] private bool _isCrouch = false;
    public bool IsCrouch { get 
        { 
            return _isCrouch;
        } 
        private set
        {
            _isCrouch = value;
            animator.SetBool(AnimationStrings.isCrouch, value);
        } 
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        } 
    }

    public bool CanMove { get {
        return animator.GetBool(AnimationStrings.canMove);
    } }


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    public bool isMoving { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate() 
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        // if (!IsCrouch) {
            if (context.started)
            {
                IsRunning = true;
            }
            else if (context.canceled)
            {
                IsRunning = false;
            }
        // }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO: check if alive
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
         if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }   
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsCrouch = true;
        }
        else if (context.canceled)
        {
            IsCrouch = false;
        }
    }
}
