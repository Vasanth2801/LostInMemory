using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;
    public SpellSO currentSpell;

    public bool canCastSpell => Time.time >= nextCastTime;
    private float nextCastTime;

    [Header("Spark Variables")]
    public float sparkRadius = 5f;
    public int sparkDamage = 20;
    public LayerMask enemyLayer;
    public float sparkCooldown = 5f;

    public GameObject sparkEffectPrefab;

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

        Spark();
    }

    void Spark()
    {
        if(!canCastSpell)
        {
            return;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, sparkRadius, enemyLayer);

        if(enemies != null)
        {
            foreach (Collider2D enemy in enemies)
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    health.ChangeHealth(-sparkDamage);
                }

                if(sparkEffectPrefab != null)
                {
                    GameObject newFX = Instantiate(sparkEffectPrefab, enemy.transform.position, Quaternion.identity);
                    Destroy(newFX, 2f);
                }
            }
            nextCastTime = Time.time + sparkCooldown;
        }
    }
}