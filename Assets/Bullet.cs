using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletColor { Red, Blue }
    public BulletColor color; // Set this in Inspector for each bullet prefab

    [Header("Settings")]
    public float speed = 30f;
    public float lifetime = 3f; // Auto-destroy after 3 seconds
    public GameObject hitEffect; // Optional particle effect

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);

        // Visual feedback (optional)
        GetComponent<Renderer>().material.color = 
            color == BulletColor.Red ? Color.red : Color.blue;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Spawn hit effect if assigned
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        // Let the Target handle the collision logic
        Destroy(gameObject);
    }
    
}