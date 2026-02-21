using UnityEngine;

public class PartFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; 
    [SerializeField] private float followSpeed; 

    private void LateUpdate()
    {
        Vector2 targetPosition = Vector2.Lerp(transform.position, targetTransform.position, followSpeed * Time.deltaTime); 
        transform.position = targetPosition; 
    }
}
