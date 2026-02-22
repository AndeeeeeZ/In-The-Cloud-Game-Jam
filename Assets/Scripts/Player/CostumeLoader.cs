using UnityEngine;

public class CostumeLoader : MonoBehaviour
{
    [SerializeField] private PlayerCostume costume;
    [SerializeField] private SpriteRenderer cloudBase, bottom, rightSide, leftSide, face;

    private void Start()
    {
        UpdateCostume();
    }

    public void UpdateCostume()
    {
        cloudBase.sprite = costume.GetCloudBase();
        bottom.sprite = costume.GetBottom();
        rightSide.sprite = costume.GetRight();
        leftSide.sprite = costume.GetLeft();
        face.sprite = costume.GetFace();
    }
}
