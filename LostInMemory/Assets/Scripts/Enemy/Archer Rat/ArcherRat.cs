using UnityEngine;

public class ArcherRat : MonoBehaviour
{
    [Header("Chasing Settings")]
    [SerializeField] float enemySpeed = 5f;
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed = 0.025f;

    [Header("References")]
    private Rigidbody2D rb;
    public Health health;

    [Header("Distance for the enemy for shoot")]
    [SerializeField] float distanceToShoot = 5f;
    [SerializeField] float distanceToStop = 2f;

    [Header("Firing rate for the Enemy")]
    [SerializeField] float fireRate;
    [SerializeField] float timer = 0;

    [Header("References for bullet and firePoint")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject arrowPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = fireRate;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(target == null)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }

        if (Vector2.Distance(transform.position, target.position) <= distanceToShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(timer <= 0)
        {
            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            timer = fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position,target.position) <= distanceToStop)
        {
            rb.linearVelocity = transform.forward * enemySpeed * Time.fixedDeltaTime;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(0, 0, angle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotationSpeed);
    }

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
        Debug.Log("Attack Taken");
    }

    void HandleDeath()
    {
        Debug.Log("Death Happenend");
        Destroy(gameObject);
    }
}