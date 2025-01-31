﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private float startingHealth;
    [SerializeField]
    private float maxHealth;

    //Amount of time in seconds to be invulnerable after taking damage;
    [SerializeField]
    private float gracePeriod;

    public float CurrentHealth { get; protected set; }
    public float MaxHealth { get { return maxHealth; } }
    public bool IsDead { get { return CurrentHealth <= 0f; } }

    public bool CanBeDamaged = true;

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
        //TODO: Should add a grace period?
        if (CanBeDamaged)
        {
            ChangeHealth(-health);
            StartCoroutine(StartGracePeriod());
        }
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
            //TODO: Should Destroy?
            Destroy(gameObject);
        }
    }

    IEnumerator StartGracePeriod()
    {
        CanBeDamaged = false;
        yield return new WaitForSeconds(gracePeriod);
        CanBeDamaged = true;
    }
}
