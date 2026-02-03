using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Health health;

    [Header("Death FX")]
    [SerializeField] private GameObject[] deathParts;
    [SerializeField] private float spawnForce = 1f;
    [SerializeField] private float torqueForce = 5f;
    [SerializeField] private float lifeTime = 2f;

    private void OnEnable()
    {
        health.onDamaged += HandleDamaged;
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.onDamaged -= HandleDamaged;
        health.OnDeath -= HandleDeath;
    }

    void HandleDamaged()
    {
        animator.SetTrigger("isDamage");
    }

    void HandleDeath()
    {
        foreach(GameObject prefab in deathParts)
        {
            Quaternion rotation = Quaternion.Euler(0,0, Random.Range(0.5f,1f)).normalized;
            GameObject part = Instantiate(prefab, transform.position, rotation);

            Rigidbody2D rb = part.GetComponent<Rigidbody2D>();

            Vector2 randomDirection = new Vector2(Random.Range(-1,1),Random.Range(-1,1)).normalized;
            rb.linearVelocity = randomDirection * spawnForce;
            rb.AddTorque(Random.Range(-torqueForce, torqueForce), ForceMode2D.Impulse);

            Destroy(part, lifeTime);
        }

        Destroy(gameObject);
    }
}
