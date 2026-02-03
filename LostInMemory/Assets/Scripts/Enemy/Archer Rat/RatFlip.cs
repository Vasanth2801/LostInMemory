using UnityEngine;

public class RatFlip : MonoBehaviour
{
    public GameObject player;
    public Health health;

    public bool isFlipped = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z = -1;

        if(transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if(transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }

    private void OnEnable()
    {
        health.onDamaged += HandleDamaged;
        health.OnDeath -= HandleDeath;
    }

    private void OnDisable()
    {
        health.onDamaged += HandleDamaged;
        health.OnDeath -= HandleDeath;
    }   

    void HandleDamaged()
    {
        Debug.Log("Animation will play");
    }

    void HandleDeath()
    {
        Debug.Log("Archer Died");
        Destroy(gameObject);
    }
}