using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PowerUp
{
    public float amountToHeal;

    public override void Apply()
    {
        var healthComponent = GetComponent<Health>();
        healthComponent.Heal(amountToHeal);
    }

    public override void Remove()
    {
        Destroy(this);
    }

    void Start()
    {
        Apply();
        Remove();
    }
}
