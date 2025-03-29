using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    float xRotation = 0f;

    void Start()
    {
        // Lock cursor to game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical look (up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation

        // Apply rotations
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Camera up/down
        transform.Rotate(Vector3.up * mouseX); // Player body left/right
    }
}