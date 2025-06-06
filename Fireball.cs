using UnityEngine;
using UnityEngine.EventSystems;

public class Fireball : MonoBehaviour
{
    public float speed = 0.5f;
    public GameObject player;
    private Vector3 playerInitialPos;
    private Vector2 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInitialPos = player.transform.position;
        Vector2 direction = playerInitialPos - transform.position;
        moveDirection = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
