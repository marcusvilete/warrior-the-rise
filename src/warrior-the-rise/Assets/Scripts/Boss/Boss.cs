using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    Animator animator;

    public event Action OnBossIntroFinished;
    public event Action OnBossVulnerabilityFinished;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void BossIntro()
    {
        StartCoroutine(StartBossIntro());
    }

    public void BecomeVulnerable()
    {
        StartCoroutine(StartBossVulnerable());
    }


    private IEnumerator StartBossVulnerable()
    {
        animator.Play("Vulnerable");

        var x = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(x.length);

        OnBossVulnerabilityFinished?.Invoke();
    }

    private IEnumerator StartBossIntro()
    {
        animator.Play("Intro");

        var x = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(x.length);

        OnBossIntroFinished?.Invoke();
    }


}
