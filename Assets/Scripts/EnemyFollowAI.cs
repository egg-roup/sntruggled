using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class EnemyFollowAI : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3.5f;
    public float stoppingDistance = 2f;
    public bool useSmoothRotation = true;
    public float rotationSpeed = 1f;

    public float attackRange = 2.5f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    public AudioClip footStepSound;
    public float footStepDelay = 0.5f;

    private NavMeshAgent agent;
    private AudioSource audioSource;
    private float nextFootstep = 0;

    private bool isAttacking = false;


    private PlayerHealth playerHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();

        agent.speed = followSpeed;
        agent.stoppingDistance = stoppingDistance;

        if (player != null)
            playerHealth = player.GetComponentInChildren<PlayerHealth>();

    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Follow player
        if (distanceToPlayer > attackRange)
        {
            agent.SetDestination(player.position);

            if (useSmoothRotation)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }

        // Footstep Sound
        if (agent.velocity.magnitude > 0.1f && distanceToPlayer > attackRange)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                audioSource.PlayOneShot(footStepSound, 0.7f);
                nextFootstep = footStepDelay;
            }
        }
        else
        {
            nextFootstep = 0;
        }

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
        {
            StartCoroutine(PerformAttackWithDelay());
        }
    }

    IEnumerator PerformAttackWithDelay()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.3f); 

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (playerHealth != null && distanceToPlayer <= attackRange)
        {
            playerHealth.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
            Debug.Log("Enemy attacked the player!");
        }

        isAttacking = false;
    }

}
