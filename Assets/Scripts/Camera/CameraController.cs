using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;

    private float currentZoom = 10f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        if (playerTarget == null)
        {
            playerTarget = FindObjectOfType<PlayerController>().transform;
        }
    }

    void LateUpdate()
    {
        if (playerTarget == null) return;

        HandleInput();
        UpdateCameraPosition();
        UpdateCameraRotation();
    }

    private void HandleInput()
    {
        // Zoom
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void UpdateCameraPosition()
    {
        Vector3 targetPosition = playerTarget.position + offset * (currentZoom / 10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }

    private void UpdateCameraRotation()
    {
        transform.LookAt(playerTarget.position + Vector3.up * 1.5f);
    }
}
