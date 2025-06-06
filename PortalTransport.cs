using System;
using UnityEngine;

public class PortalTransport : MonoBehaviour
{
    private bool inRange = false;
    public Transform spawn;
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            player.position = spawn.position;
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
