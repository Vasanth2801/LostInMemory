using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Speed")]
    [SerializeField] float speed = 7f;
    Rigidbody2D rb;

    [Header("Bullet Timings")]
    [SerializeField] float lifeOfBullet = 4f;
    float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        timer = lifeOfBullet;
        if(rb != null)
        {
            rb.linearVelocity = transform.up * speed;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}