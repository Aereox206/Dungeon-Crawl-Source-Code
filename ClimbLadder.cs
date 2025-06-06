using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public float climbSpeed = 5f;
    public int playerNumber;
    private bool isClimbing = false;
    private float verticalInput;
    private Rigidbody2D rb;
    private CircleCollider2D ladderCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ladderCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //verticalInput = Input.GetAxisRaw("Vertical");

        if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.W)) verticalInput = 1f;
            else if (Input.GetKeyUp(KeyCode.W)) verticalInput = 0f;
            if (Input.GetKeyDown(KeyCode.S)) verticalInput = -1f;
            else if (Input.GetKeyUp(KeyCode.S)) verticalInput = 0f;
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) verticalInput = 1f;
            else if (Input.GetKeyUp(KeyCode.UpArrow)) verticalInput = 0f;
            if (Input.GetKeyDown(KeyCode.DownArrow)) verticalInput = -1f;
            else if (Input.GetKeyUp(KeyCode.DownArrow)) verticalInput = 0f;
        }

        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalInput * climbSpeed);
            rb.gravityScale = 0;  // Disable gravity while climbing
            ladderCollider.isTrigger = true;
        }
        else
        {
            rb.gravityScale = 2;  // Restore gravity when not climbing
            ladderCollider.isTrigger = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
