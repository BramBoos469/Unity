using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        [Header("Chase Settings")]
        public Transform player;  // Reference to the player
        public float chaseDistance = 15f;  // The maximum distance within which the enemy will chase the player

        [Header("Wander Settings")]
        public float wanderRadius = 10f;  // Radius within which the enemy can wander
        public float wanderDirectionChangeInterval = 3f;  // Time between direction changes while wandering
        private Vector3 wanderDirection;  // Direction the mimic will move in while wandering
        private float timeSinceLastDirectionChange = 0f;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            SetRandomWanderDirection();  // Set an initial random direction for wandering
        }

        void Update()
        {
            // Check the distance between the enemy (this object) and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within the chase distance, chase the player
            if (distanceToPlayer <= chaseDistance)
            {
                ChasePlayer();
            }
            else
            {
                Wander();
            }

            // Adjust height to maintain body height from the ground
            AdjustHeight();
        }

        // Method for chasing the player
        void ChasePlayer()
        {
            // Calculate direction towards the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Interpolate movement to make it smoother
            velocity = Vector3.Lerp(velocity, new Vector3(directionToPlayer.x, 0, directionToPlayer.z) * speed, velocityLerpCoef * Time.deltaTime);

            // Assign velocity to the mimic to ensure leg placement
            myMimic.velocity = velocity;

            // Move the mimic towards the player
            transform.position = transform.position + velocity * Time.deltaTime;
        }

        // Method for wandering around
        void Wander()
        {
            // Update time since the last direction change
            timeSinceLastDirectionChange += Time.deltaTime;

            // Change direction if it's time to do so
            if (timeSinceLastDirectionChange > wanderDirectionChangeInterval || IsNearWall())
            {
                SetRandomWanderDirection();
                timeSinceLastDirectionChange = 0f;
            }

            // Interpolate movement to make it smoother
            velocity = Vector3.Lerp(velocity, wanderDirection * speed, velocityLerpCoef * Time.deltaTime);

            // Assign velocity to the mimic to ensure leg placement
            myMimic.velocity = velocity;

            // Move the mimic in the wandering direction
            transform.position = transform.position + velocity * Time.deltaTime;
        }

        // Method to set a random direction for wandering
        void SetRandomWanderDirection()
        {
            // Pick a random direction in the XZ plane
            wanderDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        // Method to detect if the mimic is near a wall or obstacle
        bool IsNearWall()
        {
            RaycastHit hit;
            // Cast a ray forward to detect walls
            if (Physics.Raycast(transform.position, wanderDirection, out hit, 2f))
            {
                // If a wall or obstacle is detected within 2 units, return true
                return true;
            }
            return false;
        }

        // Method to adjust height based on raycast to the ground
        void AdjustHeight()
        {
            RaycastHit hit;
            Vector3 destHeight = transform.position;

            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
            {
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            }

            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }
    }
}
