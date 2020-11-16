using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    public float invulnerabilityTimeInSeconds;
    private Health healthComponent;
    private Player playerComponent;
    bool isApplied;

    public override void Apply()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.CanBeDamaged = false;
        playerComponent = GetComponent<Player>();
        playerComponent.shieldRenderer.enabled = true;
        isApplied = true;
    }

    public override void Remove()
    {
        healthComponent.CanBeDamaged = true;
        playerComponent.shieldRenderer.enabled = false;
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
