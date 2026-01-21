using UnityEngine;

[CreateAssetMenu(menuName = "Spell/TeleportSO")]
public class TelelportSO : SpellSO
{
    [Header("Teleport Specific")]
    public float spellCastRange = 7f;
    public float playerRadius = 1.5f;
    public LayerMask obstacleLayer;

    public override void Cast(Player player)
    {
        Vector2 direction = new Vector2(player.facingDirection, 0);

        Vector2 targetPosition = (Vector2)player.transform.position + direction * spellCastRange;

        Collider2D hit = Physics2D.OverlapCircle(targetPosition, playerRadius, obstacleLayer);

        if (hit != null)
        {
            float step = 0.1f;
            Vector2 checkPosition = targetPosition;

            while (hit != null && Vector2.Distance(checkPosition, player.transform.position) > 0)
            {
                checkPosition -= direction * step;
                hit = Physics2D.OverlapCircle(checkPosition, playerRadius, obstacleLayer);
            }
            targetPosition = checkPosition;
        }
        player.transform.position = targetPosition;
    }
}