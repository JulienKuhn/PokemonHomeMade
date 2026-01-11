using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CameraController controller;

    private void Awake()
    {
        instance = this;
    }

    public void SetupCamera(Vector2 newMinBounds, Vector2 newMaxBounds)
    {
        controller.SetupCamera(newMinBounds, newMaxBounds);
    }

    public void LockCameraOnPlayer()
    {
        controller.SetupCamera(Vector2.zero, Vector2.zero, true);
    }
}
