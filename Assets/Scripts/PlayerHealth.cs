using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public bool isDead { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("You died! Restarting Game.");

        Invoke(nameof(RestartGame), 4f);

    }

    void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

}
