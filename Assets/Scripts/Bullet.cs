using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DummyTarget dummy = collision.gameObject.GetComponent<DummyTarget>();
            if (dummy != null)
                dummy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
