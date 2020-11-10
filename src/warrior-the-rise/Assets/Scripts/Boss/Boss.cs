using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform bossIntroOverlayPrefab;
    Animator animator;
    Health bossHealth;

    SpriteRenderer spriteRenderer;

    public event Action OnBossIntroFinished;
    public event Action OnBossVulnerabilityFinished;
    public event Action OnBossDeath;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<Health>();
        bossHealth.OnDeath += BossHealth_OnDeath;
        animator.enabled = false;
        spriteRenderer.enabled = false;

    }

    private void BossHealth_OnDeath(Health h)
    {
        OnBossDeath?.Invoke();
    }

    public void BossIntro()
    {
        StartCoroutine("StartBossIntro");
    }

    public void BecomeVulnerable()
    {
        StartCoroutine("StartBossVulnerable");
    }


    private IEnumerator StartBossVulnerable()
    {
        //Fade in
        yield return Fade(spriteRenderer, 1.0f, 3.0f);

        bossHealth.CanBeDamaged = true;
        animator.Play("Vulnerable");

        //TODO: change this so we dont get to hardcode de animation time here
        yield return new WaitForSeconds(5f);

        bossHealth.CanBeDamaged = false;

        //Fade out
        yield return Fade(spriteRenderer, 0.0f, 3.0f);


        OnBossVulnerabilityFinished?.Invoke();
    }

    private IEnumerator StartBossIntro()
    {
        yield return new WaitForSeconds(2);

        var introOverlay = Instantiate(bossIntroOverlayPrefab);

        yield return new WaitForSeconds(4.217f);

        Destroy(introOverlay.gameObject);

        spriteRenderer.enabled = true;
        animator.enabled = true;
        animator.Play("Intro");

        var x = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(x.length);

        //Fade out
        yield return Fade(spriteRenderer, 0.0f, 3.0f);

        OnBossIntroFinished?.Invoke();

    }

    private IEnumerator TakeDamage()
    {
        //Cancel `Vulnerability` state and start `Damaged` state
        StopCoroutine("StartBossVulnerable");

        bossHealth.TakeDamage(1);
        bossHealth.CanBeDamaged = false;

        animator.Play("Damaged");
        
        //TODO: change this so we dont get to hardcode de animation time here
        yield return new WaitForSeconds(1.5f);

        //Fade out
        yield return Fade(spriteRenderer, 0.0f, 3.0f);

        //Finish Vulnerability
        OnBossVulnerabilityFinished?.Invoke();
    }

    private IEnumerator Fade(SpriteRenderer sp, float aValue, float aTime)
    {
        float alpha = sp.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            sp.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            if (bossHealth.CanBeDamaged)
            {
                c.GetComponent<PlayerMovement>().PlayAttackAnimation();
                StartCoroutine("TakeDamage");
            }
        }
    }
}
