using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //TODO: change for images?
    public TextMeshProUGUI Lives;
    public LevelController levelController;
    public GameObject winUI;
    public GameObject loseUI;

    public Image[] playerHealthBar;
    public Image[] bossHealthBar;

    void Awake()
    {
        levelController = FindObjectOfType<LevelController>();

        foreach (Image img in playerHealthBar)
        {
            img.enabled = false;
        }

        foreach (Image img in bossHealthBar)
        {
            img.enabled = false;
        }
    }

    public void OnPlayerHealthChanged(HealthChangedData data)
    {
        UpdateHealthValue(data.newHealth, data.maxHealth, playerHealthBar);
    }

    public void OnBossHealthChanged(HealthChangedData data)
    {
        UpdateHealthValue(data.newHealth, data.maxHealth, bossHealthBar);
    }

    public void UpdateHealthValue(float newValue, float maxHealth, Image[] healthBar)
    {
        //Lives.text = $"Lives: {newValue.ToString("00")}/{maxHealth.ToString("00")}";

        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].enabled = i < newValue;
            
        }
        
        
    }

    public void PauseClick()
    {
        levelController.TogglePause();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void RestartLevel()
    {
        GameManager.Instance.LoadLevel("TemplateLevel");
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void ShowWinUI()
    {
        winUI.SetActive(true);
    }

    public void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }
}
