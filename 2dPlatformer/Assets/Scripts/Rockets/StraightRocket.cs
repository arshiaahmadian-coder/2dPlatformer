using UnityEngine;

public class StraightRocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float HeadPos;
    [SerializeField] private float LegPos;
    [SerializeField] private float activationDistance;
    [SerializeField] private float removeDistance;
    [SerializeField] private Transform explosionPos;
    [SerializeField] private GameObject explosionObj;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        float _speed = speed * Time.deltaTime;
        transform.Translate(_speed, 0, 0);

        float distance = transform.position.x - playerTransform.position.x;
        if (distance  <= removeDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("player hitted");
        }

        Instantiate(explosionObj, explosionPos.position, explosionPos.rotation);
        Destroy(gameObject);
    }
}
