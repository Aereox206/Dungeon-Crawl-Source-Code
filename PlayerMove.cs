using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    // double jump variables
    public bool hasDoubleJump = false;
    public bool canDoubleJump = false;

    // swap variables
    public bool hasSwap = false;
    public float swapCooldown = 5f;
    private float lastSwapTime = -999f;

    // dash variables
    private float lastLeftTapTime;
    private float lastRightTapTime;
    public bool hasDash = false;
    private float doubleTapTime = 0.2f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    private bool canDash = true;
    private bool isDashing = false; 
    private int dashDirection = 0;  
    private float dashEndTime = 0f; 

    // Player movement
    public float movementSpeed;
    private float moveInput;
    public Transform respawn;
    public int playerNumber;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool grounded;
    private Animator anim;
    public float maxVelocity;

    // layers
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    // Wall grab powerup variables
    private bool wallJumpRequested = false;
    private float wallJumpLockoutTime = 0.2f;
    private float wallJumpLockoutTimer = 0f;
    public bool hasWallGrab = false;
    public float wallGrabSlideSpeed = 0.5f;

    // Keys
    public int keyCount = 0;

    // Player number
    public bool isPlayer1;

    // Level 3
    public GameObject ghostGameArea;

    // TileAccessController reference
    private TileAccessController[] tileAccessControllers;
    public Ghost ghost;
    public bool isInGhostArea = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        // Find all TileAccessControllers in the scene
        tileAccessControllers = FindObjectsOfType<TileAccessController>();
        if (tileAccessControllers.Length == 0)
        {
            // Debug.LogWarning("No TileAccessControllers found in scene.");
        }
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleDashInput();
        HandleSwapInput();
    }

    private void FixedUpdate()
    {
        capVelocity();
        CheckGrounded();
        if (isDashing)
        {
            dash();
        }
        else
        {
            Move();
            NormalizeSlope();
        }
        WallGrab();
    }

    private void capVelocity()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
    }

    private void dash()
    {
        if (Time.time < dashEndTime)
        {
            // Apply dash velocity during the dash duration
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);
        }
        else
        {
            // End the dash when the duration is over
            isDashing = false;
        }
    }

    private void HandleMovementInput()
    {
        moveInput = 0f;
        anim.SetBool("Walking", false);

        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveInput = -1f;
                Vector3 newScale = new Vector3(-1, 1, 1);
                transform.localScale = newScale;
                anim.SetBool("Walking", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveInput = 1f;
                Vector3 newScale = new Vector3(1, 1, 1);
                transform.localScale = newScale;
                anim.SetBool("Walking", true);
            }
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveInput = -1f;
                Vector3 newScale = new Vector3(-1, 1, 1);
                transform.localScale = newScale;
                anim.SetBool("Walking", true);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveInput = 1f;
                Vector3 newScale = new Vector3(1, 1, 1);
                transform.localScale = newScale;
                anim.SetBool("Walking", true);
            }
        }
    }

    private void HandleJumpInput()
    {
        if (grounded)
        {
            anim.SetBool("Jumping Up", false);
            anim.SetBool("Jumping Down", false);
        }
        else if (rb.linearVelocity.y > 0)
        {
            anim.SetBool("Jumping Up", true);
            anim.SetBool("Jumping Down", false);
        }
        else if (rb.linearVelocity.y < 0)
        {
            anim.SetBool("Jumping Up", false);
            anim.SetBool("Jumping Down", true);
        }

        bool jumpPressed = false;

        if (playerNumber == 1)
            jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
        else if (playerNumber == 2)
            jumpPressed = Input.GetKeyDown(KeyCode.UpArrow);

        if (jumpPressed)
        {
            if (grounded || (hasDoubleJump && canDoubleJump))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 9f);

                if (!grounded && hasDoubleJump)
                    canDoubleJump = false;
            }
            else if (isTouchingWall())
            {
                wallJumpRequested = true;
            }
        }
    }

    private bool isTouchingWall()
    {
        Vector2 dir = moveInput < 0 ? Vector2.left : Vector2.right;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, dir, 0.6f, whatIsWall);
        return wallHit.collider != null && moveInput != 0;
    }

    private void HandleDashInput()
    {
        if (!hasDash || !canDash)
            return;

        float now = Time.time;

        if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (now - lastLeftTapTime < doubleTapTime && lastLeftTapTime > 0)
                {
                    StartDash(-1);
                }
                lastLeftTapTime = now;
                lastRightTapTime = 0; // Reset opposite direction tap time
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (now - lastRightTapTime < doubleTapTime && lastRightTapTime > 0)
                {
                    StartDash(1);
                }
                lastRightTapTime = now;
                lastLeftTapTime = 0; // Reset opposite direction tap time
            }
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (now - lastLeftTapTime < doubleTapTime && lastLeftTapTime > 0)
                {
                    StartDash(-1);
                }
                lastLeftTapTime = now;
                lastRightTapTime = 0; // Reset opposite direction tap time
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (now - lastRightTapTime < doubleTapTime && lastRightTapTime > 0)
                {
                    StartDash(1);
                }
                lastRightTapTime = now;
                lastLeftTapTime = 0; // Reset opposite direction tap time
            }
        }
    }

    private void HandleSwapInput()
    {
        if (!hasSwap || Time.time - lastSwapTime < swapCooldown)
            return;

        bool swapKeyPressed = (playerNumber == 1 && Input.GetKeyDown(KeyCode.Q)) ||
                              (playerNumber == 2 && Input.GetKeyDown(KeyCode.Slash));

        if (swapKeyPressed)
        {
            PlayerMove[] players = Object.FindObjectsByType<PlayerMove>(FindObjectsSortMode.None);
            foreach (var other in players)
            {
                if (other != this)
                {
                    Vector3 temp = other.transform.position;
                    other.transform.position = transform.position;
                    transform.position = temp;
                    lastSwapTime = Time.time;
                    break;
                }
            }
        }
    }

    private void StartDash(int direction)
    {
        canDash = false;
        isDashing = true;
        dashDirection = direction;
        dashEndTime = Time.time + dashDuration;
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Move()
    {
        if (!isDashing) // Only move normally if not dashing
        {
            rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);
        }
    }

    private void WallGrab()
    {
        if (wallJumpLockoutTimer > 0f)
        {
            wallJumpLockoutTimer -= Time.fixedDeltaTime;
            if (wallJumpLockoutTimer < 0f)
                wallJumpLockoutTimer = 0f;
        }

        if (!hasWallGrab || grounded || wallJumpLockoutTimer > 0f)
            return;

        Vector2 dir = moveInput < 0 ? Vector2.left : Vector2.right;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, dir, 0.6f, whatIsWall);

        if (wallHit.collider != null && moveInput != 0)
        {
            if (wallJumpRequested)
            {
                rb.linearVelocity = new Vector2(-dir.x * movementSpeed * 1.2f, 9f);
                wallJumpRequested = false;
                wallJumpLockoutTimer = wallJumpLockoutTime;
                canDoubleJump = false;
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallGrabSlideSpeed);
            }
        }
    }

    private void CheckGrounded()
    {
        
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1f + extraHeight, whatIsGround);
        grounded = false; // Reset grounded state

        if (raycastHit.collider != null)
        {
            // Check if the hit object has a TileAccessController (restricted tilemap)
            TileAccessController tileController = raycastHit.collider.GetComponent<TileAccessController>();
            if (tileController != null)
            {
                // Restricted tilemap: only ground if this is the allowed player
                if (tileController.IsAllowed(gameObject))
                {
                    grounded = true;
                    if (hasDoubleJump)
                    {
                        canDoubleJump = true;
                    }
                    // Debug.Log($"{gameObject.name} is grounded on restricted tilemap. Can double jump: {canDoubleJump}");
                }
            }
            else
            {
                // Unrestricted tilemap: ground for all players
                grounded = true;
                if (hasDoubleJump)
                {
                    canDoubleJump = true;
                }
                // Debug.Log($"{gameObject.name} is grounded on unrestricted tilemap. Can double jump: {canDoubleJump}");
            }
        }
        

        /*
        foreach (LayerMask ground in whatIsGround)
        {
            float extraHeight = 0.1f;
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1f + extraHeight, ground);
            grounded = raycastHit.collider != null;
            if (grounded && hasDoubleJump)
            {
                canDoubleJump = true;
            }
        }
        */
    }

    private void NormalizeSlope()
    {
        if (!grounded)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, playerCollider.bounds.extents.y + 0.5f, whatIsGround);
        //RaycastHit2D hit = Physics2D.Raycast()
        /*
        foreach (LayerMask ground in whatIsGround)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, playerCollider.bounds.extents.y + 0.5f, ground);
        }
        */

        if (hit.collider != null)
        {
            Vector2 normal = hit.normal;
            Vector2 tangent = new Vector2(normal.y, -normal.x);
            float moveAmount = moveInput * movementSpeed;
            Vector2 targetVelocity = tangent * moveAmount;
            rb.linearVelocity = new Vector2(targetVelocity.x, rb.linearVelocity.y);

            if (rb.linearVelocity.y <= 0f)
            {
                float gravityForce = Physics2D.gravity.y * rb.gravityScale;
                float slopeGravityCompensation = normal.x * gravityForce;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - slopeGravityCompensation * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // For Level 3
        if (collision.CompareTag("GhostArea1") || collision.CompareTag("GhostArea2"))
        {
            isInGhostArea = true;
        }

        if ((gameObject.CompareTag("Player 1") && collision.CompareTag("Fireball1")) ||
            (gameObject.CompareTag("Player 1") && collision.CompareTag("Ghost1")))
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = respawn.position;

            // So that player doesn't die on purpose to hit lever
            // Ghost respawns as well if it kills player while both are inside ghost area
            if (ghostGameArea != null)
            {
                if (isInGhostArea)
                {
                    ghost.transform.position = ghost.GetComponent<Ghost>().ghostInitialPos;
                }
            }
        }
        else if ((gameObject.CompareTag("Player 2") && collision.CompareTag("Fireball2")) ||
                 (gameObject.CompareTag("Player 2") && collision.CompareTag("Ghost2")))
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = respawn.position;

            // So that player doesn't die on purpose to hit lever
            // Ghost respawns as well if it kills player while both are inside ghost area
            if (ghostGameArea != null)
            {
                if (isInGhostArea)
                {
                    ghost.transform.position = ghost.GetComponent<Ghost>().ghostInitialPos;
                }
            }
        }
        
        // For all levels
        if (collision.CompareTag("Trap"))
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = respawn.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GhostArea1" || collision.tag == "GhostArea2")
        {
            isInGhostArea = false;
        }
    }
}