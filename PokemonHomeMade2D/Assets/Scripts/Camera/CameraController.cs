using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -310);
    private Vector3 currentVelocity = Vector3.zero;
    private bool isLocked = false;

    [Header("Boundaries")]
    [SerializeField] private bool useBoundaries = true;
    [SerializeField] private Vector2 minBounds = new Vector2(-10, -5);
    [SerializeField] private Vector2 maxBounds = new Vector2(10, 5);

    public void SetupCamera(Vector2 newMinBounds, Vector2 newMaxBounds, bool lockCamera = false)
    {
        isLocked = lockCamera;
        minBounds = newMinBounds;
        maxBounds = newMaxBounds;

        Vector3 targetPosition = target.position + offset;

        if (useBoundaries)
            targetPosition = ClampPosition(targetPosition);

        transform.position = targetPosition;
    }

    void LateUpdate()
    {
        if (target == null || isLocked) return;

        Vector3 targetPosition = target.position + offset;

        if (useBoundaries)
            targetPosition = ClampPosition(targetPosition);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private Vector3 ClampPosition(Vector3 pos)
    {
        // Using Vector2 components to clamp the X and Y coordinates
        float clampedX = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        return new Vector3(clampedX, clampedY, pos.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (!useBoundaries) return;

        Gizmos.color = Color.red;

        // Calculate the center and size of the boundary box for visualization
        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2, (minBounds.y + maxBounds.y) / 2, transform.position.z);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 1);

        Gizmos.DrawWireCube(center, size);
    }
}