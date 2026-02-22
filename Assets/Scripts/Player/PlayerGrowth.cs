using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    [SerializeField] private float baseExpPerCloud, scaleUpSpeed;
    [SerializeField] private Transform spritesParent;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private IntEvent OnPlayerSizeChanged; 

    public int Size { get; private set; } = 1; // Size used for size comparison
    private float actualSize = 1f; // Size used for scaling
    private Vector3 targetScale;

    private void Start()
    {
        targetScale = spritesParent.localScale;
    }
    private void Update()
    {
        // Smoothly lerp cloud to target size
        spritesParent.localScale = Vector3.Lerp(spritesParent.localScale, targetScale, scaleUpSpeed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if cloud
        if (!collision.TryGetComponent<Cloud>(out var cloud))
            return;

        // Check if player is big enough to eat that cloud
        if (Size < cloud.Size)
            return;

        // Calculate exp gain
        float expGain = baseExpPerCloud / (5f * Mathf.Pow(10f, Size - cloud.Size));

        // Update visual size
        actualSize += expGain;
        targetScale = Vector3.one * SizeScaleRatio(actualSize);

        // If size have incremented by 1
        // Zoom out camera
        if (actualSize > Size + 1)
        {
            cameraController.SetScaleTo(SizeScaleRatio(Size));
            Size++;
            OnPlayerSizeChanged.Raise(Size); 
        }

        cloud.Despawn();
    }

    public float SizeScaleRatio(float size)
    {
        return Mathf.Pow(1.5f, size);
    }
    public float SizeScaleRatio()
    {
        return SizeScaleRatio(Size); 
    }
}
