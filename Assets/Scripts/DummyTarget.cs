using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public float health = 100f;
    public float healingAmount = 5f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining: {health}");

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} is destroyed! Healing player.");

        Destroy(gameObject);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponentInChildren<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healingAmount, playerHealth.maxHealth);
                Debug.Log($"Player healed by {healingAmount}. New health: {playerHealth.currentHealth}");
            }

        }

    }

}
