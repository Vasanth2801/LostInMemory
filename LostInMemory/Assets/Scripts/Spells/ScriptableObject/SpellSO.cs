using UnityEngine;

public abstract class SpellSO : CollectableSO
{
    [Header("General Spell Info")]
    public float cooldownTime;

    public override void Collect(Player player)
    {
        player.magic.LearnSpell(this);
    }

    public abstract void Cast(Player player);
}
