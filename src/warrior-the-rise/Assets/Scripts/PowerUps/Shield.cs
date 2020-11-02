using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    public float invulnerabilityTimeInSeconds;
    private Health healthComponent;

    public override void Apply()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.CanBeDamaged = false;
    }

    public override void Remove()
    {
        healthComponent.CanBeDamaged = true;
        Destroy(this);
    }

    IEnumerator Start()
    {
        Apply();
        yield return new WaitForSeconds(invulnerabilityTimeInSeconds);
        Remove();
    }
}
