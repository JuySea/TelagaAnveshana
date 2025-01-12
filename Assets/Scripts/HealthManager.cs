using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Maksimum health
    [SerializeField] private int currentHealth; // Health saat ini

    [Header("UI Elements")]
    [SerializeField] private Image healthBarFill; // Referensi ke Health Fill Bar

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth); // Batasi nilai antara 0 dan maxHealth
            UpdateHealthUI(); // Perbarui UI setiap kali currentHealth berubah
        }
    }

    void Start()
    {
        CurrentHealth = maxHealth; // Set initial health
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage; // Kurangi health menggunakan property
        if (CurrentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount; // Tambahkan health menggunakan property
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Player is dead!");
        // Implement death logic
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        CurrentHealth = maxHealth; // Reset current health to max health
    }

    // Memastikan UI diperbarui saat nilai berubah di Inspector
    private void OnValidate()
    {
        UpdateHealthUI();
    }
}
