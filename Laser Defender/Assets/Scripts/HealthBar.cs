using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider = default;
    [SerializeField] Gradient gradient = default;
    [SerializeField] Image fill = default;

    private Player player;
    
    public void Start()
    {
        player = FindObjectOfType<Player>();
        SetMaxHealth(player.GetHealth());
    }

    public void UpdateHealth(int playerHealth)
    {
        slider.value = playerHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int playerHealth)
    {
        slider.maxValue = playerHealth;
        slider.value = playerHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
