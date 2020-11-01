using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private float startingHealth;
    [SerializeField]
    private float maxHealth;


    public float CurrentHealth { get; protected set; }
    public float MaxHealth { get { return maxHealth; } }
    public bool IsDead { get { return CurrentHealth <= 0f; } }

    //Events
    public event Action<Health> OnDeath;
    public event Action<HealthChangedData> OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = startingHealth;
    }

    public void Heal(float health)
    {
        ChangeHealth(health);
    }

    public void TakeDamage(float health)
    {
        //TODO: Play hit particles?
        ChangeHealth(-health);
    }

    public void ChangeHealth(float health)
    {
        if (IsDead)
        {
            return;
        }

        var eventData = new HealthChangedData
        {
            maxHealth = maxHealth,
            oldHealth = CurrentHealth
        };

        CurrentHealth += health;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
        eventData.newHealth = CurrentHealth;
        OnHealthChanged?.Invoke(eventData);

        if (IsDead)
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
