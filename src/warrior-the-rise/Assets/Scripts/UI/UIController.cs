using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //TODO: change for images?
    public TextMeshProUGUI Lives;

    public void OnChangeHealth(HealthChangedData data)
    {
        UpdateHealthValue(data.newHealth, data.maxHealth);
    }

    public void UpdateHealthValue(float newValue, float maxHealth)
    {
        Lives.text = $"Lives: {newValue.ToString("00")}/{maxHealth.ToString("00")}";
    }
}
