using UnityEngine;

public class TrapGhost : MonoBehaviour
{
    public GameObject trapZone;
    public GameObject lever;
    public GameObject player;
    private Ghost ghost;
    public bool isInArea = false;
    public bool isTrapped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ghost = GetComponent<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever.GetComponent<Lever>().on && isInArea && !player.GetComponent<PlayerMove>().isInGhostArea)
        {
            ghost.enabled = false;
            isTrapped = true;
        }
        else
        {
            ghost.enabled = true;
            isTrapped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trapZone.tag == collision.tag)
        {
            isInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (trapZone.tag == collision.tag)
        {
            isInArea = false;
        }
    }
}
