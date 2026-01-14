using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public int facingDirection = 1;
    [SerializeField] PlayerInput playerInput;
    public Rigidbody2D rb;

    [Header("Inputs")]
    public Vector2 moveInput;

    void Update()
    {
        Flip();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float targetSpeed = moveInput.x * speed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        if(moveInput.x < 0.1f)
        {
            facingDirection = 1;
        }
        if(moveInput.x > -0.1f)
        {
            facingDirection = -1;
        }
    }
}