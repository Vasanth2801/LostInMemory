using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public  PlayerWallJumpState(Player player) : base(player) { }

    public float horizontalJumpPercent = 0.5f;

    public override void Enter()
    {
        base.Enter();

        animator.SetBool("isWallJumping", true);
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = new Vector2(-player.facingDirection * horizontalJumpPercent, 1f) * player.jumpMultiplier;

        JumpPressed = false;
        JumpReleased = false;
    }

    public override void Update()
    {
        base.Update();

        if (!player.isGrounded && player.isTouchingWall && MoveInput.x == player.facingDirection && rb.linearVelocity.y > 0)
        {
            player.ChangeState(player.wallSlideState);
        }
        else if (JumpPressed && player.isTouchingWall)
        {
            player.ChangeState(player.wallJumpState);
        }
        else if(player.isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.ApplyGravity();

        if(JumpReleased && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x,rb.linearVelocity.y) * player.jumpMultiplier;
            JumpReleased = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool("isWallJumping", false);
    }
}
