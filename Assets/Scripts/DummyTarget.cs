using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DummyTarget : MonoBehaviour
{
    private Animator animator;


    public float health = 100f;
    public float healingAmount = 5f;

    private bool isDead = false;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    public void TakeDamage(float amount)
    {

        if (isDead) return;

        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining: {health}");


        if (health <= 0)
            Die();
    }


    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{gameObject.name} is destroyed! Healing player.");


        if (ScoreManager.scoreManager != null) {
            ScoreManager.scoreManager.AddKill();
        }


        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.SetDestination(transform.position);


        EnemyFollowAI ai = GetComponent<EnemyFollowAI>();
        if (ai != null)
            ai.enabled = false;




        if (animator != null)
        {
            animator.SetTrigger("Die");
            StartCoroutine(DelayedDestroy(0.75f));
        }
        else
        {
            Destroy(gameObject);
        }
       
        IEnumerator DelayedDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }




        GameObject player = GameObject.FindGameObjectWithTag("Player");


        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponentInChildren<PlayerHealth>();
           
            if (playerHealth != null)
            {
                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healingAmount, playerHealth.maxHealth);
                playerHealth.healthBar.SetHealth(playerHealth.currentHealth);
                Debug.Log($"Player healed by {healingAmount}. New health: {playerHealth.currentHealth}");
            }


        }


    }


}
