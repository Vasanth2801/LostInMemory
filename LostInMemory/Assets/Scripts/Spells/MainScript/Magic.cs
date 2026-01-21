using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;
    public SpellSO currentSpell;

    [Header("CoolDown")]
    public bool canCastSpell => Time.time >= nextCastTime;
    private float nextCastTime;

    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    void CastSpell()
    {
        if(!canCastSpell || currentSpell == null)
        {
            return; 
        }

        currentSpell.Cast(player);

        nextCastTime = Time.time + currentSpell.cooldownTime;
    }
}