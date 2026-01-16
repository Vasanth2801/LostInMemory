using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("PLayer Modular States")]

    public PlayerState currentState;

    public PlayerIdle idleState;












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
    [SerializeField] bool isGrounded;

    [Header("SlideSettings")]
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float slideSpeed = 12f;
    [SerializeField] private float slideStopDuration = 0.2f;
    
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

    private void Start()
    {
        rb.gravityScale = normalGravity;

        
    }

    void Update()
    {
        currentState.Update();

        Flip();
        HandleAnimations();
        HandleSlide();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();

        ApplyGravity();
        CheckGrounded();

        if (!isSliding)
        {
            HandleMovement();
        }

        HandleJump();
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

    void HandleSlide()
    {

        if(isSliding)
        {
            slideTimer -= Time.deltaTime;

            if(slideTimer <= 0)
            {
                isSliding = false;
            }
        }


        if(isGrounded && runPressed && moveInput.y < -0.1f && !isSliding)
        {
            isSliding = true;
            slideTimer = slideDuration;
            rb.linearVelocity = new Vector2(slideSpeed * facingDirection,rb.linearVelocity.y);
        }
    }
    
    void HandleMovement()
    {
        float currentSpeed = runPressed ? runSpeed : walkSpeed;
        float targetSpeed = moveInput.x * currentSpeed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if(jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
            jumpReleased = false;
        }
       if(jumpReleased)
        {
            if (rb.linearVelocity.y > 0.1f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpMultiplier);
            }
            jumpReleased = false;
        }
    }

    void ApplyGravity()
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
        bool isMoving = Mathf.Abs(moveInput.x) > 0.1f && isGrounded;

        animator.SetBool("isIdle", !isMoving && isGrounded && !isSliding);

        animator.SetBool("isWalking", isMoving && !runPressed && !isSliding);

         animator.SetBool("isRunning", isMoving && runPressed && !isSliding);

        animator.SetBool("isJumping",rb.linearVelocity.y >0.1f);

        animator.SetBool("isSliding", isSliding);

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