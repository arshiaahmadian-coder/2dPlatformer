using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        animator.SetBool("isRunning", inputX != 0);
        // animator.SetFloat("Speed", Mathf.Abs(inputX));

        if (inputX > 0)
            transform.localScale = new Vector3(3.5f, transform.localScale.y, transform.localScale.z);
        else if (inputX < 0)
            transform.localScale = new Vector3(-3.5f, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * moveSpeed, 0);
    }
}
