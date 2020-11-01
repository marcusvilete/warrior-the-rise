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

    private Health playerHealth;

    void Awake()
    {
        //Setup the scene
        
        //TODO: Not in play mode yet!

        //Force portrait mode
        ForcePortrait();

        //1- Set camera to safe area
        SetCamera();

        //2- Scale playable area
        ScalePlayableArea();

        //
        //3- Spawn Player
        SpawnPlayer();
        //

        //4- Initialize UI
        InitializeUI();


        //
        //4- Load Level data
        LoadLevelData();

        //TODO: Set to playmode!
    }

    private void InitializeUI()
    {
        //Instantiate Health UI, bind events to the player
        var ui = Instantiate(uiPrefab);
        ui.UpdateHealthValue(playerHealth.CurrentHealth, playerHealth.CurrentHealth);
        playerHealth.OnHealthChanged += ui.OnChangeHealth;
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
        playerHealth =  Instantiate(playerPrefab, playableArea);
    }

    
    private void LoadLevelData()
    {
        //TODO: load data for spawning projectiles and stuff;
    }
}
