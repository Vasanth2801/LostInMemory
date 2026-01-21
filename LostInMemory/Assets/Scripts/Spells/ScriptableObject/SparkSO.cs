using UnityEngine;

[CreateAssetMenu(menuName = "Spell/AOE")]
public class SparkSO : SpellSO
{
    [Header("AOE Spell Specific")]
    public float sparkRadius = 5f;
    public int sparkDamage = 20;
    public LayerMask enemyLayer;

    public GameObject sparkEffectPrefab;

    public override void Cast(Player player)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, sparkRadius, enemyLayer);

        if (enemies != null)
        {
            foreach (Collider2D enemy in enemies)
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    health.ChangeHealth(-sparkDamage);
                }

                if (sparkEffectPrefab != null)
                {
                    GameObject newFX = Instantiate(sparkEffectPrefab, enemy.transform.position, Quaternion.identity);
                    Destroy(newFX, 2f);
                }
            }
        }
    }
}