using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    private void Update()
    {
        transform.position = new Vector3(
            targetTransform.position.x, targetTransform.position.y, transform.position.z);
    }
}
