using UnityEngine;

[CreateAssetMenu(menuName = "PlayerCostume")]
public class PlayerCostume : ScriptableObject
{
    // index 0 = none
    public int cloudBaseIndex, bottomIndex, rightIndex, leftIndex, faceIndex;
    public Sprite[] cloudBases, bottoms, rightSides, leftSides, faces;

    [SerializeField] private VoidEvent OnCostumeChanged;

    public Sprite GetCloudBase() => GetFrom(cloudBases, cloudBaseIndex);
    public Sprite GetBottom() => GetFrom(bottoms, bottomIndex);
    public Sprite GetRight() => GetFrom(rightSides, rightIndex);
    public Sprite GetLeft() => GetFrom(leftSides, leftIndex);
    public Sprite GetFace() => GetFrom(faces, faceIndex);

    // diff = 1 => get next
    // diff = -1 => get prev
    // diff = 0 => current
    public void CycleCloudBase(int diff) => CycleIndex(cloudBases, ref cloudBaseIndex, diff);
    public void CycleBottom(int diff) => CycleIndex(bottoms, ref bottomIndex, diff);
    public void CycleRight(int diff) => CycleIndex(rightSides, ref rightIndex, diff);
    public void CycleLeft(int diff) => CycleIndex(leftSides, ref leftIndex, diff);
    public void CycleFace(int diff) => CycleIndex(faces, ref faceIndex, diff);

    private Sprite GetFrom(Sprite[] arr, int index)
    {
        if (arr == null || arr.Length == 0) return null;
        return arr[index];
    }

    private void CycleIndex(Sprite[] arr, ref int index, int diff)
    {
        if (arr == null || arr.Length == 0) return;

        index += diff;

        if (index < 0)
            index = arr.Length - 1;
        else if (index >= arr.Length)
            index = 0;

        OnCostumeChanged.Raise();
    }

    public void GetRandom()
    {
        cloudBaseIndex = Random.Range(0, cloudBases.Length); 
        bottomIndex = Random.Range(0, bottoms.Length); 
        faceIndex = Random.Range(0, faces.Length); 
        leftIndex = Random.Range(0, leftSides.Length); 
        rightIndex = Random.Range(0, rightSides.Length); 

        OnCostumeChanged.Raise(); 
    }


    public void Reset()
    {
        cloudBaseIndex = bottomIndex = rightIndex = leftIndex = faceIndex = 0;
        OnCostumeChanged.Raise();
    }
}