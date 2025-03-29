using UnityEngine;

public class Target : MonoBehaviour
{
    public enum BoardColor { Red, Blue }
    public BoardColor boardColor;
    public Material originalMaterial;
    [HideInInspector] public bool wasHit = false; // Added declaration with default value

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        wasHit = false; // Explicit initialization
    }

    void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet == null) return;

        bool correctHit = (bullet.color.ToString() == boardColor.ToString());
        
        // Visual feedback
        GetComponent<Renderer>().material = GameManager.Instance.hitMaterial;
        wasHit = true;
        gameObject.SetActive(false);
        
        // Notify GameManager
        if (GameManager.Instance != null) // Added null check
        {
            GameManager.Instance.RegisterHit(this, correctHit);
        }
        
        Destroy(bullet.gameObject);
    }
}