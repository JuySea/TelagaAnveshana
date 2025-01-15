using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Healthbar : MonoBehaviour
{
    private PlayerStats player;
    public Slider HealthSlider;
    public Slider StaminaSlider;

    private void Start()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerStats>();

        HealthSlider.interactable = false;
        StaminaSlider.interactable = false;

        Health_Update();
        Stamina_Update();

        player.onStaminaChanged += Stamina_Update;
        player.onHealthChanged += Health_Update;
    }


    private void Health_Update()
    {
        HealthSlider.maxValue = player.GetMaxHealthValue();
        HealthSlider.value = player.currentHealth;
    }

    private void Stamina_Update()
    {
        StaminaSlider.maxValue = player.GetMaxStaminaValue();
        StaminaSlider.value = player.stamina;
    }

    private void OnDisable()
    {
        player.onStaminaChanged -= Stamina_Update;
        player.onHealthChanged -= Health_Update;
    }

}
