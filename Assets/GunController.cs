using UnityEngine;

public class GunController : MonoBehaviour
{
    // Assign these in Unity Inspector
    public GameObject redBulletPrefab;
    public GameObject blueBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click (red bullet)
        {
            Shoot(redBulletPrefab);
        }
        else if (Input.GetMouseButtonDown(1)) // Right-click (blue bullet)
        {
            Shoot(blueBulletPrefab);
        }
    }

    void Shoot(GameObject bulletPrefab)
    {
        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Add velocity
        bullet.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * bulletSpeed;
        
        // Auto-destroy after 3 seconds
        Destroy(bullet, 1f);
    }
}