using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    Animator animator;
    Health bossHealth;

    public event Action OnBossIntroFinished;
    public event Action OnBossVulnerabilityFinished;
    public event Action OnBossDeath;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<Health>();
        bossHealth.OnDeath += BossHealth_OnDeath;
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
        bossHealth.CanBeDamaged = true;
        animator.Play("Vulnerable");

        //TODO: change this so we dont get to hardcode de animation time here
        yield return new WaitForSeconds(5f);

        bossHealth.CanBeDamaged = false;
        OnBossVulnerabilityFinished?.Invoke();
    }

    private IEnumerator StartBossIntro()
    {
        animator.Play("Intro");

        var x = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(x.length);

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

        
        //Finish Vulnerability
        OnBossVulnerabilityFinished?.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            if (bossHealth.CanBeDamaged)
            {
                StartCoroutine("TakeDamage");
            }
        }
    }

}
