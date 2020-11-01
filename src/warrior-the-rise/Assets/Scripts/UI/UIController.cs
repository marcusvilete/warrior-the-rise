using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //TODO: change for images?
    public Text Lives;

    public void OnChangeHealth(HealthChangedData data)
    {
        UpdateHealthValue(data.newHealth, data.oldHealth);
    }

    public void UpdateHealthValue(float newValue, float oldValue)
    {
        Lives.text = $"Lives: {newValue}/{oldValue}";
    }
}
