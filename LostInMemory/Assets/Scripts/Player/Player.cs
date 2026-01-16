using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public int facingDirection = 1;

    [Header("References")]
    [SerializeField] PlayerInput playerInput;
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpMultiplier = 0.5f;
    public float normalGravity;
    public float fallGravity;
    public float jumpGravity;
    [SerializeField] bool jumpPressed;
    [SerializeField] bool jumpReleased;

    [Header("GroundCheckSettings")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    [SerializeField] bool isGrounded;

    [Header("Inputs")]
    public Vector2 moveInput;
    private bool runPressed;

    private void Start()
    {
        rb.gravityScale = normalGravity;
    }

    void Update()
    {
        Flip();
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        CheckGrounded();
        HandleMovement();
        HandleJump();
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

    void HandleAnimations()
    {
        bool isMoving = Mathf.Abs(moveInput.x) > 0.1f && isGrounded;

        animator.SetBool("isIdle", !isMoving && isGrounded);

        animator.SetBool("isWalking", isMoving && !runPressed);

         animator.SetBool("isRunning", isMoving && runPressed);

        animator.SetBool("isJumping",rb.linearVelocity.y >0.1f);

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