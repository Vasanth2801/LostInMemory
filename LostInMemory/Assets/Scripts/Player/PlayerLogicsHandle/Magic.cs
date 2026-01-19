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

    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    void CastSpell()
    {
        Teleport();
    }

    void Teleport()
    {
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
}