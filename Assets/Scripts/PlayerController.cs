using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private bool _isMoving = false;
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

    [SerializeField]
    private bool _isRunning = false;

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
        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
            walkSpeed = 10f;
        }
        else if (context.canceled)
        {
            IsRunning = false;
            walkSpeed = 5f;
        }
    }
}
