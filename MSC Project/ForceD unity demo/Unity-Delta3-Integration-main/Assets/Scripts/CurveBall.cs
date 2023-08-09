using UnityEngine;

public class BubbleMotion : MonoBehaviour
{
    public float speed = 0.5f;
    public float frequency = 0.5f;
    public float magnitude = 3.0f;
    public Vector3 movementAreaSize = new Vector3(5f, 5f, 5f);

    private Vector3 initialPosition;
    private float timeOffset;

    public GameObject dotFront; 
    public GameObject dotBehind;

    private void Start()
    {
        initialPosition = transform.position;
        timeOffset = Random.Range(0f, 100f); // Randomize the starting point of the Perlin noise.
    }


    private void Update()
    {
        Vector3 newPosition = initialPosition;

        // Calculate the Perlin noise values for each axis.
        float xNoise = Mathf.PerlinNoise(Time.time * frequency + timeOffset, 0) * 2 - 1;
        float yNoise = Mathf.PerlinNoise(0, Time.time * frequency + timeOffset) * 2 - 1;
        float zNoise = Mathf.PerlinNoise(Time.time * frequency + timeOffset, Time.time * frequency + timeOffset) * 2 - 1;

        // Apply the Perlin noise values to the position within the specified movement area.
        newPosition += new Vector3(xNoise, yNoise, zNoise) * magnitude;
        newPosition = Vector3.Scale(newPosition - initialPosition, movementAreaSize) + initialPosition;

        // Clamp the newPosition values to ensure they stay within the specified range.
        newPosition.x = Mathf.Clamp(newPosition.x, -10f, 10f);
        newPosition.y = Mathf.Clamp(newPosition.y, -10f, 10f);
        newPosition.z = Mathf.Clamp(newPosition.z, -10f, 10f);

        // Move the object smoothly.
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);

        // Update the positions of the dotFront and dotBehind objects.
        UpdateDotPosition(dotFront, newPosition, Vector3.forward);
        UpdateDotPosition(dotBehind, newPosition, Vector3.back);
    }

    private void UpdateDotPosition(GameObject dot, Vector3 spherePosition, Vector3 offsetDirection)
    {
        // Calculate the position for the dot based on the sphere's position and the offset direction.
        Vector3 dotPosition = spherePosition + offsetDirection * 2.0f; // You can adjust the offset distance here.
        dot.transform.position = dotPosition;
    }

}
