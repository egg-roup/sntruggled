using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowAI : MonoBehaviour
{
    public Transform player;  
    public float followSpeed = 3.5f; 
    public float stoppingDistance = 0;  
    public bool useSmoothRotation = true;
    public float rotationSpeed = 5f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        agent.speed = followSpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        if (player == null)
            return;

        agent.SetDestination(player.position);

        if (useSmoothRotation && agent.remainingDistance > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
