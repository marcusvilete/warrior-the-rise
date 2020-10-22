using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed = 10;
    void Awake()
    {
        rb.velocity = Vector2.up * movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
