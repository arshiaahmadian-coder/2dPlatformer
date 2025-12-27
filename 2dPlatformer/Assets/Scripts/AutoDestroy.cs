using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Invoke(nameof(DestroyIt), lifeTime);
    }

    private void DestroyIt()
    {
        Destroy(gameObject);
    }
}
