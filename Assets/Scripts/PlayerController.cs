using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    Vector2 moveInput;
    Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private bool _isMoving = false;
    public bool IsMoving { get 
        { 
            return _isMoving;
        } 
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        } 
    }

    public float currentSpeed { get
        {
            if(IsMoving)
            {
                if(IsRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            } else {
                // If the player is not moving, the speed is 0
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
            animator.SetBool("isRunning", value);
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


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public bool isMoving { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() 
    {
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);
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
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
}
