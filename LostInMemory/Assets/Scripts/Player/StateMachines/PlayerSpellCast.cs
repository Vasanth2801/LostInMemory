using UnityEngine;

public class PlayerSpellCast : PlayerState
{
    public PlayerSpellCast(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("isCasting", true);
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();

        if(Mathf.Abs(MoveInput.x) > 0.1f)
        {
            player.ChangeState(player.moveState);
        }
        else
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool("isCasting", false);
    }
}
