using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Camera cam;
    public Transform playableArea;
    public Health playerPrefab;
    public UIController uiPrefab;
    public SpawnSystem spawnSystem;
    public BossEncounter bossEncounter;
    public Boss bossPrefab;


    public LevelState currentLevelState;
    public event Action<LevelState> GameStateChanged;


    private Health playerHealth;
    private UIController uiController;
    void Awake()
    {

        SetupLevel();
    }


    private void SetupLevel()
    {
        //Setup the scene
        Time.timeScale = 1;
        //Not in play mode yet!
        currentLevelState = LevelState.Loading;
        GameStateChanged?.Invoke(currentLevelState);

        //Force portrait mode
        ForcePortrait();

        //1- Set camera to safe area
        SetCamera();

        //2- Scale playable area
        ScalePlayableArea();

        //3- Initialize UI
        InitializeUI();

        //
        //4- Spawn Player
        SpawnPlayer();
        //



        //
        //5- Load Level data
        LoadLevelData();

        currentLevelState = LevelState.Playing;
        GameStateChanged?.Invoke(currentLevelState);
    }

    private void InitializeUI()
    {
        uiController = Instantiate(uiPrefab);
    }

    private void ForcePortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void SetCamera()
    {
        float width = (float)Screen.safeArea.width / Screen.width;
        float height = (float)Screen.safeArea.height / Screen.height;
        float x = (Screen.safeArea.x / Screen.width);
        float y = (Screen.safeArea.y / Screen.height);
        cam.rect = new Rect(x, y, width, height);
    }

    private void ScalePlayableArea()
    {
        float screenRatio = (float)Screen.safeArea.width / Screen.safeArea.height;
        playableArea.localScale = playableArea.localScale * AspectHelper.GetScaleFactor(screenRatio);
    }

    private void SpawnPlayer()
    {
        playerHealth = Instantiate(playerPrefab, playableArea);
        uiController.UpdateHealthValue(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        playerHealth.OnHealthChanged += uiController.OnChangeHealth;
        playerHealth.OnDeath += PlayerHealth_OnDeath;
    }

    private void PlayerHealth_OnDeath(Health playerHealth)
    {
        TogglePause();
        uiController.ShowLoseUI();
    }

    private void LoadLevelData()
    {
        //TODO: pass level as a parameter?
        spawnSystem.LevelLoaded += SpawnSystem_LevelLoaded;
        spawnSystem.LevelFinished += SpawnSystem_LevelFinished;
        spawnSystem.LoadLevel("Level1");
    }

    private void SpawnSystem_LevelLoaded()
    {
        Debug.Log("Level Loaded");
        spawnSystem.StartLevel();
    }

    private void SpawnSystem_LevelFinished()
    {
        //TODO: start boss encounter
        Debug.Log("Level Finished");

        //cleanup
        spawnSystem.LevelFinished -= SpawnSystem_LevelFinished;
        spawnSystem.LevelLoaded -= SpawnSystem_LevelFinished;

        SetupBoss();

        
    }

    private void SetupBoss()
    {
        var boss = Instantiate(bossPrefab, playableArea);
        boss.OnBossDeath += Boss_OnBossDeath;
        bossEncounter.StartEncounter(boss, spawnSystem);

    }

    private void Boss_OnBossDeath()
    {
        TogglePause();
        uiController.ShowWinUI();
    }

    public void TogglePause()
    {
        if (currentLevelState == LevelState.Playing)
        {
            Time.timeScale = 0;
            currentLevelState = LevelState.Paused;
            GameStateChanged?.Invoke(currentLevelState);
        }
        else if (currentLevelState == LevelState.Paused)
        {
            Time.timeScale = 1;
            currentLevelState = LevelState.Playing;
            GameStateChanged?.Invoke(currentLevelState);
        }
    }
}
