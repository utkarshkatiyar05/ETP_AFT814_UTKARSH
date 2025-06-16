using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    public float knockbackForce = 20f;
    public float knockbackAngle = 45f;
    public LayerMask knockbackLayer;

    private bool isDashing;
    private float dashTimer;
    private Vector2 dashDirection;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Move left/right
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        }
        if (Input.GetKeyDown(KeyCode.E) && !isDashing)
        { 
            isDashing = true;
            dashTimer = dashDuration;
            dashDirection = new Vector2(transform.localScale.x, 0).normalized;
        }
    }

    void OnDrawGizmosSelected()
    {
       if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
