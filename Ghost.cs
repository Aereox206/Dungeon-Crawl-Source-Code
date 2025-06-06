using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject player;
    public float speed = 6f; 
    public Vector3 ghostInitialPos;
    private Vector3Int playerInitialPos;
    private bool canChasePlayer = false;
    public Grid grid;

    public GameObject fireballPrefab;
    public float fireInterval = 2f;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ghostInitialPos = transform.position;
        playerInitialPos = grid.WorldToCell(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        if (canChasePlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer >= fireInterval)
            {
                Fire();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
            transform.position = Vector3.MoveTowards(transform.position, ghostInitialPos, speed * Time.deltaTime);
        }
    }

    // if player is inside starting area, ghosts can't chase it
    void ChasePlayer()
    {
        Vector3Int playerPos = grid.WorldToCell(player.transform.position);
        
        if (player.GetComponent<PlayerMove>().isPlayer1) {
            if (playerPos.x < playerInitialPos.x + 2 && playerPos.x >= playerInitialPos.x &&
            playerPos.y < playerInitialPos.y + 2 && playerPos.y >= playerInitialPos.y)
            {
                canChasePlayer = false;
            }
            else canChasePlayer = true;
        }
        else
        {
            if (playerPos.x > playerInitialPos.x - 2 && playerPos.x <= playerInitialPos.x &&
            playerPos.y < playerInitialPos.y + 2 && playerPos.y >= playerInitialPos.y)
            {
                canChasePlayer = false;
            }
            else canChasePlayer = true;
        }
    }

    void Fire()
    { 
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Animator anim = fireball.GetComponent<Animator>();

        if (player.transform.position.x <= transform.position.x)
        {
            anim.SetBool("isGoingRight", false);
        }
        else anim.SetBool("isGoingRight", true);

        // set the player object of the fireball equal to the player object of this ghost
        Fireball fb = fireball.GetComponent<Fireball>();
        anim = fireball.GetComponent<Animator>();
        fb.player = player;
    }
}
