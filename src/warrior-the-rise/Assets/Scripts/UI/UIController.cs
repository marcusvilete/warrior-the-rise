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

    void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void OnChangeHealth(HealthChangedData data)
    {
        UpdateHealthValue(data.newHealth, data.maxHealth);
    }

    public void UpdateHealthValue(float newValue, float maxHealth)
    {
        Lives.text = $"Lives: {newValue.ToString("00")}/{maxHealth.ToString("00")}";
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
