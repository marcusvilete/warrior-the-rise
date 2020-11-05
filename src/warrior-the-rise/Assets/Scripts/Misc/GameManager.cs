using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update

    public GameState currentGameState;
    public event Action<GameState> GameStateChanged;

    private void SetState(GameState state)
    {
        currentGameState = state;
        GameStateChanged?.Invoke(currentGameState);
    }

    public void LoadMainMenu()
    {
        if (currentGameState != GameState.MainMenu)
        {
            SceneManager.LoadScene("MainMenu");
            SetState(GameState.MainMenu);
        }
    }

    public void LoadLevel(string name)
    {
        if (currentGameState != GameState.Quitting)
        {
            SceneManager.LoadScene(name);
            SetState(GameState.Playing);
        }
    }
}



