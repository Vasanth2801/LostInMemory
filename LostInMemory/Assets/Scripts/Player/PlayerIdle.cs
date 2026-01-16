using NUnit.Framework;
using UnityEngine;

public abstract class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player) : base(player)
    {

    }

    public override void Enter()
    {
        animator.SetBool("isIdle", true);
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        animator.SetBool("isIdle", false);
    }
}