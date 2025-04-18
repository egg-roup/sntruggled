using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f;
    
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        
        // Ensure we have a collider
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            // Make sure it's not a trigger
            col.isTrigger = false;
        }
        
        // Set up the rigidbody for better collision detection
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            // Don't use gravity for bullets
            rb.useGravity = false;
        }
        
        // Add debug trail to visualize bullet path
        Debug.DrawRay(transform.position, rb.velocity.normalized * 10f, Color.red, lifeTime);
        
        // Debug log
        Debug.Log($"Bullet spawned at {transform.position} with velocity {rb.velocity}");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // Prevent multiple hits
        hasHit = true;
        
        Debug.Log($"Bullet collided with: {collision.gameObject.name} at {collision.contacts[0].point}");
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DummyTarget dummy = collision.gameObject.GetComponent<DummyTarget>();
            if (dummy != null)
            {
                dummy.TakeDamage(damage);
                Debug.Log($"Dealt {damage} damage to {collision.gameObject.name}");
            }
        }

        Destroy(gameObject);
    }
    
    // Add additional collision check using raycasts
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && rb.velocity.magnitude > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rb.velocity.normalized, out hit, rb.velocity.magnitude * Time.fixedDeltaTime + 0.1f))
            {
                Debug.Log($"Raycast detected hit with: {hit.collider.gameObject.name}");
                
                // Handle the hit manually
                if (hit.collider.CompareTag("Enemy"))
                {
                    DummyTarget dummy = hit.collider.GetComponent<DummyTarget>();
                    if (dummy != null)
                    {
                        dummy.TakeDamage(damage);
                        Debug.Log($"Dealt {damage} damage to {hit.collider.gameObject.name} via raycast");
                    }
                }
                
                Destroy(gameObject);
            }
        }
    }
}