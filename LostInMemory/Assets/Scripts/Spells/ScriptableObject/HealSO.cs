using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Heal Spell")]
public class HealSO : SpellSO
{
    [Header("Heal Spell Specific")]
    public int healAmount;
    public GameObject healEffectPrefab;

    public override void Cast(Player player)
    {
        GameObject nexFX = Instantiate(healEffectPrefab, player.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Destroy(nexFX, 2f);
        player.health.ChangeHealth(healAmount);
    }
}