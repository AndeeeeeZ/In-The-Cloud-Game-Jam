using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomMulti, zoomSpeed;
    [SerializeField] private CinemachineCamera cam;
    private Inputs input;
    private float normalSize;
    private float targetSize;

    private void Awake()
    {
        input = new Inputs();
    }

    private void Start()
    {
        normalSize = targetSize = cam.Lens.OrthographicSize;
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
        targetSize = normalSize * zoomMulti;
    }

    private void OnZoomOutCanceled(InputAction.CallbackContext context)
    {
        targetSize = normalSize;
    }

    private void Update()
    {
        float currentSize = cam.Lens.OrthographicSize; 
        float newSize = Mathf.Lerp(currentSize, targetSize, zoomSpeed * Time.deltaTime); 
        cam.Lens.OrthographicSize = newSize; 
    }
}
