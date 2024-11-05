using UnityEngine;

public class FastOscillation : MonoBehaviour
{
    // Speed of the oscillation
    public float speed = 20.0f;

    // Maximum distance from the original position
    public float distance = 0.5f;

    // Store the original position
    private Vector3 startPos;

    void Start()
    {
        // Save the starting position of the object
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the new position using a sine wave for smooth oscillation
        float offset = Mathf.Sin(Time.time * speed) * distance;

        // Update the position of the object
        transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);
    }
}
