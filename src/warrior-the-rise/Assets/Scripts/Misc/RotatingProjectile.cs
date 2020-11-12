using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProjectile : MonoBehaviour
{

    Vector3 rotateAround;
    float speed;
    bool isRunning = false;
    Boss bossRef;
    public float damage;

    void Update()
    {
        if (isRunning)
        {
            transform.RotateAround(rotateAround, Vector3.forward, speed * Time.deltaTime);
        }
    }

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(Scale(transform, 0, 1, 1));

    }

    IEnumerator Scale(Transform tr, float from, float to, float aTime)
    {
        Vector3 vFrom = new Vector3(from, from, from);
        Vector3 vTo = new Vector3(to, to, to);
        
        
        for (float t = 0.0f; t < 1; t += (Time.deltaTime / aTime))
        {
            tr.localScale = Vector3.Lerp(vFrom, vTo, t);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Initialize(Vector3 rotateAround, float speed, Boss bossRef)
    {
        this.bossRef = bossRef;
        this.rotateAround = rotateAround;
        this.speed = speed;
        isRunning = true;
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return Scale(transform, 1, 0, 1);
        Destroy(this.gameObject);
    }

    public void Destroy()
    {
        //Cleanup
        bossRef.OnBossVulnerabilityFinishing -= this.Destroy;

        StartCoroutine(DestroyCoroutine());
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            c.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
