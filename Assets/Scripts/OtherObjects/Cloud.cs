using UnityEngine;

public class Cloud : MonoBehaviour
{
    public int Size { get; private set; }
    private CloudObjectPool pool;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetPool(CloudObjectPool p)
    {
        pool = p;
    }
    public void Despawn()
    {
        pool.Release(gameObject);
    }

    public void Initialize(int s, Sprite sprite)
    {
        Size = s;
        transform.localScale *= Mathf.Sqrt(Size);
        sr.sprite = sprite;
    }

    public void ResetState()
    {
        transform.localScale = Vector3.one;
    }

}
