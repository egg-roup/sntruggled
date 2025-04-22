using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private GameOverMenu gameOverMenu;

    public bool isDead { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
        gameOverMenu = FindObjectOfType<GameOverMenu>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Current health: {currentHealth}");


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
