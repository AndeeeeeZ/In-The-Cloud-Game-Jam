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
    [SerializeField] private float minSpawnBuffer, maxSpawnBuffer;
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

    private void SpawnCloudOutsideOfScreen()
    {
        int side = Random.Range(0, 4);
        Vector3 viewportPos = Vector3.zero;

        float buffer = Random.Range(minSpawnBuffer, maxSpawnBuffer);

        switch (side)
        {
            case 0: viewportPos = new Vector3(Random.value, 1 + buffer, 0); break; // Top
            case 1: viewportPos = new Vector3(Random.value, -buffer, 0); break;    // Bottom
            case 2: viewportPos = new Vector3(-buffer, Random.value, 0); break;    // Left
            case 3: viewportPos = new Vector3(1 + buffer, Random.value, 0); break; // Right
        }

        Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);
        worldPos.z = 0f;

        SpawnCloudAt(worldPos);
    }

    private void SpawnCloudAt(Vector3 pos)
    {
        GameObject obj = pool.Get();
        obj.transform.position = pos;

        int size = sizes[Random.Range(0, sizes.Length)];

        size += player.Size; // Make sure clouds smaller than player stops spawning

        Sprite sprite = sprites[Random.Range(0, sprites.Length)];

        Cloud cloud = obj.GetComponent<Cloud>(); 
        cloud.Initialize(size, sprite);
    }
}
