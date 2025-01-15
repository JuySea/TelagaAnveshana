using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    [Header("Stamina")]
    public Stats maxStamina;
    public float stamina;

    public System.Action onStaminaChanged;
    private float staminaRecoveryDelay = 1.2f; 
    private float staminaRecoveryTimer = 0;   
    private bool isStaminaDepleted = false;  

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        TakeDamage(0);
        stamina = GetMaxStaminaValue();
        StaminaUsed(0);
        player = GetComponent<Player>();
        Debug.Log("stamina"+ stamina);
    }

    public int GetMaxStaminaValue()
    {
        return maxStamina.GetValue();
    }

    protected override void Update()
    {
        base.Update();
        StaminaRecovery();
    }

    public void StaminaUsed(float staminaValue)
    {
        stamina -= staminaValue;
        if (onStaminaChanged != null)
            onStaminaChanged();
    }


    private void StaminaRecovery()
    {
        if (stamina == 0 && !isStaminaDepleted)
        {
            isStaminaDepleted = true;        
            staminaRecoveryTimer = staminaRecoveryDelay;
            player.Tired();
        }

        if (isStaminaDepleted)
        {
            staminaRecoveryTimer -= Time.deltaTime;

            if (staminaRecoveryTimer <= 0)
            {
                isStaminaDepleted = false; 
            }
            else
            {
                return; 
            }
        }

        stamina += Time.deltaTime * 6.4f;
        stamina = Mathf.Clamp(stamina, 0, GetMaxStaminaValue());

        Debug.Log($"Stamina recovered. Current: {stamina}");

        if (onStaminaChanged != null)
        {
            Debug.Log("onStaminaChanged event invoked.");
            onStaminaChanged();
        }
    }



    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();

        player.Die();
    }
}
