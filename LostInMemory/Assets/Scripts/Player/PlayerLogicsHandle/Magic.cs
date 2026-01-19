using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;
    public float spellCastRange = 7f;

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

        player.transform.position = targetPosition;
    }
}
