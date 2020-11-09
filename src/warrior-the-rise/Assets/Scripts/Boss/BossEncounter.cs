using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public Boss boss;
    public SpawnSystem spawnSystem;
    int waveCounter;

    public void StartEncounter(Boss boss, SpawnSystem spawnSystem)
    {
        waveCounter = 1;
        this.spawnSystem = spawnSystem;
        this.boss = boss;

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
        boss.OnBossDeath += Boss_OnBossDeath;

        //spawn system events
        spawnSystem.LevelLoaded += SpawnSystem_LevelLoaded;
        spawnSystem.LevelFinished += SpawnSystem_LevelFinished;

    }

    private void Boss_OnBossDeath()
    {
        //??
        //See "Win/Lose" logic on level controller
    }
}
