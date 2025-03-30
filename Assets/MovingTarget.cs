using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float speed = 1f;
    public float movementRange = 2f;
    private Vector3 startPos;
    private Color originalColor; // Store original color

    void Start()
    {
        startPos = transform.position;
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(
            Mathf.PingPong(Time.time * speed, movementRange), 
            0, 
            0
        );
    }

    public void ResetColor() // Method to restore color
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}