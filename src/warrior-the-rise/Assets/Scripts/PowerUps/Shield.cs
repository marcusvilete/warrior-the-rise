using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    public float invulnerabilityTimeInSeconds;
    private Health healthComponent;
    bool isApplied;

    public override void Apply()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.CanBeDamaged = false;
        isApplied = true;
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

    private void Update()
    {
        healthComponent.CanBeDamaged = false;
    }
}
