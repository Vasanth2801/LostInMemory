using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("PLayer Modular States")]

    public PlayerState currentState;

    public PlayerIdle idleState;

    public PlayerMove moveState;

    public PlayerJump jumpState;

    public PlayerCrouch crouchState;

    public PlayerSlide slideState;


    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public int facingDirection = 1;

    [Header("References")]
    [SerializeField] PlayerInput playerInput;
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField] CapsuleCollider2D playerCollider;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpMultiplier = 0.5f;
    public float normalGravity;
    public float fallGravity;
    public float jumpGravity;

    [Header("GroundCheckSettings")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    [Header("SlideSettings")]
    public  float slideDuration = 0.5f;
    public  float slideSpeed = 12f;
    public  float slideStopDuration = 0.2f;
    
    private float slideHeight;
    public float normalHeight;
    public Vector2 normalOffset;
    public Vector2 slideOffset;

    [SerializeField] private bool isSliding = false;

    [Header("Crouch Settings")]
    [SerializeField] Transform ceilingCheck;
    [SerializeField] private float ceilingCheckRadius = 0.2f;

    [Header("Inputs")]
    public Vector2 moveInput;
    public bool runPressed;
    public bool jumpPressed;
    public bool jumpReleased;

    private void Awake()
    {
        idleState = new PlayerIdle(this);
        moveState = new PlayerMove(this);
        jumpState = new PlayerJump(this);
        slideState = new PlayerSlide(this);
        crouchState = new PlayerCrouch(this);
    }

    private void Start()
    {
        rb.gravityScale = normalGravity;

        ChangeState(idleState);
    }

    void Update()
    {
        currentState.Update();

        if (!isSliding)
        {
            Flip();
        }
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();

        CheckGrounded();
 
    }

    public void ChangeState(PlayerState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();   
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            jumpPressed = true;
            jumpReleased = false;
        }
        else
        {
            jumpReleased = true;
        }
    }

    public void OnRun(InputValue value)
    {
        runPressed = value.isPressed;
    }


    public void ApplyGravity()
    {
        if(rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = fallGravity;
        }
        else if(rb.linearVelocity.y > 0.1f)
        {
            rb.gravityScale = jumpGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);
    }

    
    public bool CheckCieling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayerMask);
    }
    
    public void SetColliderNormal()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, normalHeight);
        playerCollider.offset = normalOffset;
    }

    public void SetColliderSlide()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, slideHeight);
        playerCollider.offset = slideOffset;
    }

    void HandleAnimations()
    { 
        animator.SetBool("isGrounded", isGrounded);

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void Flip()
    {
        if(moveInput.x > 0.1f)
        {
            facingDirection = 1;
        }
        if(moveInput.x < -0.1f)
        {
            facingDirection = -1;
        }

        transform.localScale = new Vector3(facingDirection,1,1);
    }
}