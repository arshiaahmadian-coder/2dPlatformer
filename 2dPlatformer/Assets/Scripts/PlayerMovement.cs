using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float sitWalkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCD;

    [Header("Joystick Thresholds")]
    [SerializeField] private float walkThreshold = 0.3f;
    [SerializeField] private float jumpSitThreshold = 0.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Joystick joystick;
    private Animator animator;
    private bool canJump = true;
    private bool isSitting = false;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindFirstObjectByType<Joystick>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        GroundCheck();

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        float absH = Mathf.Abs(h);
        float speed = absH < 0.7f ? walkSpeed : runSpeed;

        if(v < -jumpSitThreshold)
        {
            isSitting = true;
            // speed = sitWalkSpeed;
        } else
        {
            isSitting = false;
        }
        animator.SetBool("isSitting", isSitting);

        if (absH < walkThreshold)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
        else
        {
            if(isSitting)
                rb.linearVelocity = new Vector2(Mathf.Sign(h) * sitWalkSpeed, rb.linearVelocity.y);
            else
                rb.linearVelocity = new Vector2(Mathf.Sign(h) * speed, rb.linearVelocity.y);

            animator.SetBool("isWalking", speed == walkSpeed);
            animator.SetBool("isRunning", speed == runSpeed);

            Flip(h);
        }

        if (v > jumpSitThreshold && isGrounded && canJump)
        {
            Vector2 jumpDir = new Vector2(0, 1f).normalized;

            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            isGrounded = false;
            canJump = false;
            Invoke(nameof(ResetJumpCD), jumpCD);
        }
    }

    void Flip(float h)
    {
        if (h > 0.05f)
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        else if (h < -0.05f)
            transform.localScale = new Vector3(-3f, transform.localScale.y, transform.localScale.z);
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheckPoint.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        isGrounded = hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            groundCheckPoint.position,
            groundCheckPoint.position + Vector3.down * groundCheckDistance
        );
    }

    private void ResetJumpCD()
    {
        canJump = true;
    }
}
