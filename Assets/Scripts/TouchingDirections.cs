using UnityEngine;

public class TouchingDirections : MonoBehaviour
{   
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    Rigidbody2D rb;
    CapsuleCollider2D touchingCollider;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    private Animator animator;

    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded 
    { get {
        return _isGrounded;
        } private set {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        } 
    }
    
    [SerializeField]
    private bool _isOnWall = false;
    public bool IsOnWall 
    { get {
        return _isOnWall;
        } private set {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        } 
    }
    
    [SerializeField]
    private bool _isOnCeiling = false;
    public bool IsOnCeiling
    { get {
        return _isOnCeiling;
        } private set {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        } 
    }

    private Vector2 wallCheckDirection;

    

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCollider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        wallCheckDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    }
}
