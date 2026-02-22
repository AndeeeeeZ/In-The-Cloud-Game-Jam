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

    public void Initialize(int s, Sprite sprite, PlayerGrowth p)
    {
        Size = s;
        transform.localScale = Vector3.one * p.SizeScaleRatio(Size);
        sr.sprite = sprite;
        UpdateColor(p.Size); 
    }

    public void ResetState()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPlayerSizeChanged(int playerSize)
    {
        UpdateColor(playerSize); 
    }

    private void UpdateColor(int playerSize)
    {
        if (playerSize >= Size)
            sr.color = Color.green; 
        else
            sr.color = Color.red;
    }
}
