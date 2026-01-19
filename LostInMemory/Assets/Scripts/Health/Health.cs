using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDeath;
    public event Action onDamaged;

    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
        else if(amount <= 0)
        {
            onDamaged?.Invoke();
        }
    }
}