using UnityEngine;

public class PlayerJump : PlayerState
{
    public PlayerJump(Player player) : base(player) { }
    
    public override void Enter()
    {
        base.Enter();

        animator.SetBool("isJumping", true);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);

        JumpPressed = false;
        JumpReleased = false;
    }

    public override void Update()
    {
        base.Update();

        if(!player.isGrounded && player.isTouchingWall && MoveInput.x == player.facingDirection && rb.linearVelocity.y > 0)
        {
            player.ChangeState(player.wallSlideState);
        }
        else if ((JumpPressed && player.isTouchingWall))
        {
            player.ChangeState(player.wallJumpState);
        }
        else if (player.isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.ApplyGravity();

        if(JumpReleased && rb.linearVelocity.y > 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * player.jumpMultiplier);
            JumpReleased = false;
        }

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        rb.linearVelocity = new Vector2(speed * player.facingDirection,rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        animator.SetBool("isJumping", false);
    }
}