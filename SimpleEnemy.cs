using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.localScale.x > 0) movingRight = true;
        else movingRight = false;
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        rb.linearVelocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);

        // Check for ground ahead
        bool isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        // Check for wall ahead
        bool isBlocked = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, 0.1f, groundLayer);

        if (!isGrounded || isBlocked)
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
