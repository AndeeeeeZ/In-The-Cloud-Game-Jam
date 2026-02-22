using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [Header("Cloud and Pool")]
    [SerializeField] private CloudObjectPool pool;
    [SerializeField] private int[] sizes;
    [SerializeField] private Sprite[] sprites;
    [Header("Player")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private PlayerGrowth player;
    [Header("Values")]
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnBuffer;
    [SerializeField] private int initialAmount;

    private Camera cam;

    private void Start()
    {
        // Spawn "initialAmount" amount of clouds around player 
        for (int i = 0; i < initialAmount; i++)
        {
            SpawnCloudNearPlayer();
        }

        // Call SpawnCloudOutsideOfScreen every spawnInterval
        InvokeRepeating(nameof(SpawnCloudOutsideOfScreen), 0f, spawnInterval);

        cam = Camera.main;
    }

    // Spawn near player
    private void SpawnCloudNearPlayer()
    {
        Vector2 pos = Random.insideUnitCircle * spawnRadius + (Vector2)targetTransform.position;
        SpawnCloudAt(pos);
    }
    private float GetCameraRadius()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        // Radius that fully covers the screen
        return Mathf.Sqrt(width * width + height * height) * 0.5f;
    }
    private void SpawnCloudOutsideOfScreen()
    {
        float camRadius = GetCameraRadius();
        float buffer = Random.Range(0, spawnBuffer) * player.SizeScaleRatio();

        float spawnRadius = camRadius + buffer;

        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)targetTransform.position + dir * spawnRadius;

        SpawnCloudAt(spawnPos);
    }
    private void SpawnCloudAt(Vector3 pos)
    {
        GameObject obj = pool.Get();
        obj.transform.position = pos;

        int size = sizes[Random.Range(0, sizes.Length)];
        size += player.Size; // Make sure clouds smaller than player stops spawning
        size = Mathf.Max(1, size); 

        Sprite sprite = sprites[Random.Range(0, sprites.Length)];

        Cloud cloud = obj.GetComponent<Cloud>();
        cloud.Initialize(size, sprite, player);
    }

}
