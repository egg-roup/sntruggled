using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private GameOverMenu gameOverMenu;
    public HealthBar healthBar;

    public bool isDead { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Current health: {currentHealth}");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("You died! Restarting Game.");

        gameOverMenu.ShowGameOver();
    }

}
