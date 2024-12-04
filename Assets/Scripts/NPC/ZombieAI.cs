using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float speed = 3.5f;

    private Transform player;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            agent.SetDestination(player.position);
            AvoidCrowding();
        }
    }
    private void AvoidCrowding()
    {
        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, 1f); // Radio pequeño
        Vector3 avoidanceDirection = Vector3.zero;

        foreach (Collider col in nearbyZombies)
        {
            if (col.gameObject != gameObject && col.CompareTag("Zombie"))
            {
                // Alejarse del zombie cercano
                avoidanceDirection += transform.position - col.transform.position;
            }
        }

        if (avoidanceDirection != Vector3.zero)
        {
            agent.SetDestination(transform.position + avoidanceDirection.normalized);
        }
    }

}
