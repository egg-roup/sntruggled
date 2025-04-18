using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining: {health}");

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} is destroyed!");
        Destroy(gameObject);
    }
}
