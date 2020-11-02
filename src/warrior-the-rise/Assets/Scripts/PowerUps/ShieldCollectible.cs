using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class ShieldCollectible : MonoBehaviour
{
    public float invulnerabilityTimeInSeconds;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            ApplyPowerUp(c.gameObject);

            Destroy(this.gameObject);
        }
    }

    private void ApplyPowerUp(GameObject g)
    {
        var powerUp = g.GetComponent<Shield>();

        if (powerUp != null)
        {
            Destroy(powerUp);
        }


        powerUp = g.AddComponent<Shield>();
        powerUp.invulnerabilityTimeInSeconds = invulnerabilityTimeInSeconds;
    }
}
