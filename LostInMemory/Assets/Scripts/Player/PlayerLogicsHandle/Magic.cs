using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;

    [Header("Teleport Variables")]
    public float spellCastRange = 7f;
    public float spellCastCooldown = 2f;
    public float playerRadius = 1.5f;
    public LayerMask obstacleLayer;

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
        //Teleport();
        Spark();
    }

    void Teleport()
    {
        if (!canCastSpell)
        {
            return;
        }

        Vector2 direction = new Vector2(player.facingDirection, 0);

        Vector2 targetPosition = (Vector2)player.transform.position + direction * spellCastRange;

        Collider2D hit = Physics2D.OverlapCircle(targetPosition, playerRadius, obstacleLayer);

        if(hit != null)
        {
            float step = 0.1f;
            Vector2 checkPosition = targetPosition;

            while(hit != null && Vector2.Distance(checkPosition,player.transform.position) > 0)
            {
                checkPosition -= direction * step;
                hit = Physics2D.OverlapCircle(checkPosition, playerRadius, obstacleLayer);
            }
            targetPosition = checkPosition;
        }

        player.transform.position = targetPosition;

        nextCastTime = Time.time + spellCastCooldown;
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