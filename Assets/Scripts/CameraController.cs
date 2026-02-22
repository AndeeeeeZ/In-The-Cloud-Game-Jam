using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomMulti, zoomSpeed;
    [SerializeField] private CinemachineCamera cam;
    private Inputs input;
    private float normalSize; // Default size of the camera
    private float normalScale; // Current cam size = normalSize * normalScale
    private float targetScale;

    private void Awake()
    {
        input = new Inputs();
    }

    private void Start()
    {
        normalSize = cam.Lens.OrthographicSize;
        normalScale = targetScale = 1f; 
    }

    private void OnEnable()
    {
        input.Enable();
        input.Camera.ZoomOut.performed += OnZoomOutPerformed;
        input.Camera.ZoomOut.canceled += OnZoomOutCanceled;
    }

    private void OnDisable()
    {
        input.Camera.ZoomOut.performed -= OnZoomOutPerformed;
        input.Camera.ZoomOut.canceled -= OnZoomOutCanceled;
        input.Disable();
    }

    private void OnZoomOutPerformed(InputAction.CallbackContext context)
    {
        // Zoom out 
        targetScale = normalScale * zoomMulti;
    }

    private void OnZoomOutCanceled(InputAction.CallbackContext context)
    {
        // Zoom back in
        targetScale = normalScale;
    }

    private void Update()
    {
        // Smoothly zoom
        float currentSize = cam.Lens.OrthographicSize; 
        float newSize = Mathf.Lerp(currentSize, normalSize * targetScale, zoomSpeed * Time.deltaTime); 
        cam.Lens.OrthographicSize = newSize; 
    }

    public void SetScaleTo(float scale)
    {
        targetScale *= scale / normalScale; 
        normalScale = scale; 
    }
}
