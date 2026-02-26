using System;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color eatableColor, non_eatableColor;
    public int Size { get; private set; }
    private CloudObjectPool pool;
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
        spriteRenderer.sprite = sprite;
        UpdateColor(p.Size);
    }

    public void ResetState()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPlayerSizeChanged(int playerSize)
    {
        // Despawn this cloud if it's too small compared to player
        if (playerSize - Size > 2)
        {
            Despawn();
            return;
        }
        UpdateColor(playerSize);
    }

    private void UpdateColor(int playerSize)
    {
        if (playerSize >= Size)
            spriteRenderer.color = eatableColor;
        else
            spriteRenderer.color = non_eatableColor;
    }
}
