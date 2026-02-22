using UnityEngine;
using UnityEngine.Pool;

public class CloudObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform cloudParent; // For organization in inspector
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 200;
    private ObjectPool<GameObject> pool;
    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            CreateObject,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolObject,
            false,
            defaultCapacity,
            maxSize
        );
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(prefab, cloudParent);

        Cloud cloud = obj.GetComponent<Cloud>(); 
        cloud.SetPool(this);

        obj.SetActive(false);
        return obj;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<Cloud>().ResetState(); 
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject Get()
    {
        if (pool == null)
        {
            Debug.LogWarning("ERROR: No pool found"); 
            return null; 
        }
        return pool.Get();
    }

    public void Release(GameObject obj)
    {
        pool.Release(obj);
    }
}
