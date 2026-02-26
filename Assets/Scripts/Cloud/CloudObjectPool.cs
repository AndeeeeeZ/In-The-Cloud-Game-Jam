using UnityEngine;
using UnityEngine.Pool;

public class CloudObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform cloudParent; // For organization in inspector
    [SerializeField] private int defaultCapacity = 100;
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
        if (obj == null)
        {
            Debug.LogWarning("ERROR: Trying to take an null obj"); 
            return; 
        }
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
        GameObject obj = pool.Get(); 
        if (obj == null)
            obj = CreateObject(); 
        return obj;
    }

    public void Release(GameObject obj)
    {
        pool.Release(obj);
    }
}
