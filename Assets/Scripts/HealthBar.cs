using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI healthText;

    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
        UpdateHealthText(slider.value, slider.maxValue);
    }

    public void SetHealth(float health) {
        slider.value = health;
        UpdateHealthText(slider.value, slider.maxValue);
    }

    public void UpdateHealthText(float current, float max) {
        healthText.text = Mathf.CeilToInt(current) + " / " + Mathf.CeilToInt(max);
    }
}
