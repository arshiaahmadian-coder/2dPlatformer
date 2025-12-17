using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;

    [Range(0f, 1f)]
    [SerializeField] private float walkThreshold = 0.3f;

    private Rigidbody2D rb;
    private Joystick joystick;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindFirstObjectByType<Joystick>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float h = joystick.Horizontal;
        float absH = Mathf.Abs(h);

        if (absH < walkThreshold)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            return;
        }

        float speed = absH < 0.7f ? walkSpeed : runSpeed;
        rb.linearVelocity = new Vector2(Mathf.Sign(h) * speed, rb.linearVelocity.y);

        // rotate character 180 deg in x
        Flip(h);

        // animation state manager
        if (speed == walkSpeed)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        } else if (speed == runSpeed)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
    }

    void Flip(float h)
    {
        if (h > 0.05f)
            transform.localScale = new Vector3(3.5f, transform.localScale.y, transform.localScale.z);
        else if (h < -0.05f)
            transform.localScale = new Vector3(-3.5f, transform.localScale.y, transform.localScale.z);
    }
}
