using UnityEngine;

public class PlayerMove : PlayerState
{
    public PlayerMove(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if(SpellCastPressed && magic.canCastSpell(magic.CurrentSpell))
        {
            player.ChangeState(player.spellCastState);
        }
        else if (AttackPressed && combat.canAttack)
        {
            player.ChangeState(player.attackState);
        }
        else if (JumpPressed)
        {
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(MoveInput.x) < 0.1f)
        {
            player.ChangeState(player.idleState);
        }
        else if (player.isGrounded && RunPressed && MoveInput.y < -0.1f)
        {
            player.ChangeState(player.slideState);
        }
        else
        {
            animator.SetBool("isWalking", !RunPressed);
            animator.SetBool("isRunning", RunPressed);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        float targetSpeed = speed * MoveInput.x;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }
}
