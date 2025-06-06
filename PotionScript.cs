using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public Lever lever;
    public LayerMask groundLayer;
    private bool onGround = false;
    private Rigidbody2D rb;
    private bool inRange = false;
    public Transform spawn;
    public Transform player;
    public Transform respawn;
    public SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if lever is pulled, potion will be dropped
        if (lever.on)
        {
            rb.gravityScale = 2f;
        }
        else rb.gravityScale = 0f;

        if (!onGround)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.6f, groundLayer);

            if (hit.collider != null)
            {
                onGround = true;
                rb.linearVelocity = Vector2.zero;         // Stop movement
                rb.bodyType = RigidbodyType2D.Static; // Freeze in place
            }
        }

        if (inRange && Input.GetKeyDown(KeyCode.E)) {
            player.position = spawn.position;
            respawn.position = spawn.position;
            Destroy(gameObject);
            /*
            if (sr != null)
            {
                Color color = sr.color;
                color.a = 1f;
                sr.color = color;
            }
            */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player 1")
        {
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player 1")
        {
            inRange = false;
        }
    }
}
