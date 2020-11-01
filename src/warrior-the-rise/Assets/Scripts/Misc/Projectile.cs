using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed;

    public void SetProjectileSpeed(float speed)
    {
        movespeed = speed;
        rb.velocity = Vector2.up * movespeed;
    }
}
