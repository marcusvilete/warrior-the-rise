using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public Boss boss;
    public PlayerMovement player;
    public SpawnSystem spawnSystem;
    public RotatingProjectile rotatingProjectilePrefab; 
    int waveCounter;

    public void StartEncounter(Boss boss, SpawnSystem spawnSystem)
    {
        waveCounter = 1;
        this.spawnSystem = spawnSystem;
        this.boss = boss;
        player = FindObjectOfType<PlayerMovement>();

        SetupEventHandlers();
        BossIntro();
    }


    void BossIntro()
    {
        boss.BossIntro();
    }

    private void Boss_OnBossIntroFinished()
    {
        spawnSystem.LoadLevel($"Level1_Boss{waveCounter}");
        
    }

    private void Boss_OnBossVulnerabilityFinished()
    {
        Debug.Log("OnBossVulnerabilityFinished");

        waveCounter++;

        if (waveCounter > 6)
        {
            //??
            //Do something here or just let a impossible wave of projectiles do the work?
        }
        else
        {
            spawnSystem.LoadLevel($"Level1_Boss{waveCounter}");
        }

    }

    private void SpawnSystem_LevelLoaded()
    {
        
        spawnSystem.StartLevel();
    }

    private void SpawnSystem_LevelFinished()
    {
        boss.BecomeVulnerable();
    }

    private void SetupEventHandlers()
    {
        //boss events
        boss.OnBossIntroFinished += Boss_OnBossIntroFinished;
        boss.OnBossVulnerabilityFinished += Boss_OnBossVulnerabilityFinished;
        boss.OnBossVulnerabilityStarted += Boss_OnBossVulnerabilityStarted;
        boss.OnBossVulnerabilityFinishing += Boss_OnBossVulnerabilityFinishing;
        boss.OnBossDeath += Boss_OnBossDeath;

        //spawn system events
        spawnSystem.LevelLoaded += SpawnSystem_LevelLoaded;
        spawnSystem.LevelFinished += SpawnSystem_LevelFinished;

    }

    private void Boss_OnBossVulnerabilityFinishing()
    {
        player.ResetPosition();
    }

    private void Boss_OnBossVulnerabilityStarted()
    {
        player.DisableMovement();
        player.ResetPosition();
        
        SpawnRingOfProjectiles(3, 10, Vector3.left, 2);
        SpawnRingOfProjectiles(6, 20, Vector3.right, 3);
        SpawnRingOfProjectiles(9, 30, Vector3.left, 4);
        player.EnableMovement();
    }

    private void Boss_OnBossDeath()
    {
        //??
        //See "Win/Lose" logic on level controller
    }

    private void SpawnRingOfProjectiles(float distance, int amount, Vector3 orientation, int holes)
    {
        int holeCount = 1;
        float angleStep = 360 / (float)amount;
        Vector3 position = new Vector3(0, distance);

        int holeStep = amount / holes;

        for (int i = 1; i <= amount; i++)
        {
            if (holeCount <= holes)
            {
                if (i == holeStep * holeCount)
                {
                    holeCount++;
                    continue;
                }
            }

            var proj = Instantiate(boss.rotatingProjectilePrefab, boss.transform);
            proj.transform.localPosition = position;
            proj.transform.RotateAround(boss.transform.position, Vector3.forward, i * angleStep);

            //When vulnerability is done, we self destroy
            boss.OnBossVulnerabilityFinishing += proj.Destroy;

            if (orientation == Vector3.left)
            {
                proj.Initialize(boss.transform.position, -(angleStep * 2), boss);
                
            }
            else
            {
                proj.Initialize(boss.transform.position, angleStep * 2, boss);
            }
        }
    }
}
