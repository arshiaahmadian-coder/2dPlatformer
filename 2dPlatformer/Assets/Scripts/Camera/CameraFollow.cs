using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    // [SerializeField] private float yOfset;

    private void Update()
    {
        transform.position = new Vector3(
            targetTransform.position.x, targetTransform.position.y, transform.position.z);
    }
}
