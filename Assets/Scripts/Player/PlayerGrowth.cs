using TMPro;
using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    [SerializeField] private float baseExpPerCloud, scaleUpSpeed;
    [SerializeField] private Transform spritesParent;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private IntEvent OnPlayerSizeChanged;
    [SerializeField] private TextMeshProUGUI scoreText;

    public int Size { get; private set; } = 0; // Size used for size comparison
    private float actualSize = 0f; // Size used for scaling
    private Vector3 targetScale;
    private CapsuleCollider2D col;
    private Vector2 defaultColliderSize;
    private int score = 0;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        targetScale = spritesParent.localScale;
        defaultColliderSize = col.size;
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

        UpdateScore((int)SizeScaleRatio(cloud.Size));

        // Update visual size
        actualSize += expGain;
        targetScale = Vector3.one * SizeScaleRatio(actualSize);

        // If size have incremented by 1
        // Zoom out camera
        if (actualSize > Size + 1)
        {
            Size++;
            OnPlayerSizeChanged.Raise(Size);
            cameraController.SetScaleTo(SizeScaleRatio(Size));

            // Update collider size
            col.size = defaultColliderSize * SizeScaleRatio(Size);
        }

        cloud.Despawn();
    }

    private void UpdateScore(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
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
