using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed;
    public float damage;

    private float yPositionToDestroy;

    private void Awake()
    {
        yPositionToDestroy = -(Camera.main.orthographicSize * 1.2f);
    }

    private void Update()
    {
        HandleDestruction();
    }

    private void HandleDestruction()
    {
        if (transform.position.y <= yPositionToDestroy)
        {
            //TODO: if poolable, return to pool
            Destroy(this.gameObject);
        }
    }

    public void SetProjectileSpeed(float speed)
    {
        movespeed = speed;
        rb.velocity = Vector2.down * movespeed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            //TODO: what do we do with projectile? explode? destroy? keep going?
            c.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
