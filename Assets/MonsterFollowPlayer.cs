using UnityEngine;
using UnityEngine.AI;  // Import NavMeshAgent for pathfinding

public class MonsterFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player’s position
    public float followDistance = 10f;  // The distance within which the monster will follow
    private NavMeshAgent navMeshAgent;  // NavMeshAgent component on the monster

    void Start()
    {
        // Get the NavMeshAgent component from the monster
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Calculate the distance between the monster and the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Check if the player is within the follow distance
        if (distanceToPlayer <= followDistance)
        {
            // Set the destination of the NavMeshAgent to the player’s position
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            // If the player is too far, stop moving
            navMeshAgent.SetDestination(transform.position);  // Stay in place
        }
    }
}
