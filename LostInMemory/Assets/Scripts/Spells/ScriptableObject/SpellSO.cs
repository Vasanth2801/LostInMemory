using UnityEngine;

public abstract class SpellSO : ScriptableObject
{
    [Header("General Spell Info")]
    public string spellName;
    public Sprite spellIcon;
    public float cooldownTime;

    public abstract void Cast(Player player);
}
