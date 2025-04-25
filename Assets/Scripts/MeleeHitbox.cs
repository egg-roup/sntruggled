using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public float damage = 10f;
    private bool hasHit = false;

    // Optional: reset hit tracking so the same hitbox can be reused
    public void ResetHit()
    {
        hasHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            DummyTarget dummy = other.GetComponent<DummyTarget>();
            if (dummy != null)
            {
                dummy.TakeDamage(damage);
                hasHit = true;
                Debug.Log($"Melee hit: {other.name} for {damage} damage");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Invoke(nameof(ResetHit), 0.3f); // allows next hit after short time
        }
    }

}
