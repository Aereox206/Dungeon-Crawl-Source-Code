using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject player;
    public Sprite offSprite;
    public Sprite onSprite;
    public bool on = false;
    public Lever[] interconnectedLevers;
    public Lever[] onAtOneTimeLevers;

    private bool inRange1 = false;
    private bool inRange2 = false;

    public void Update()
    {
        if ((inRange1 && Input.GetKeyDown(KeyCode.E)) || (inRange2 && Input.GetKeyDown(KeyCode.RightShift)))
        {
            toggle();
            foreach (Lever lever in interconnectedLevers)
            {
                lever.toggle();
            }
            foreach (Lever lever in onAtOneTimeLevers)
            {
                if (on && lever.on) lever.toggle();
            }
        }
    }

    public void toggle()
    {
        on = !on;
        if (on) GetComponent<SpriteRenderer>().sprite = onSprite;
        else GetComponent<SpriteRenderer>().sprite = offSprite;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // For if levers have designated players
        if (player != null)
        {
            if (collision.CompareTag("Player 1") && collision.CompareTag(player.tag))
            {
                inRange1 = true;
            }
            else if (collision.CompareTag("Player 2") && collision.CompareTag(player.tag))
            {
                inRange2 = true;
            }
        }
        // Levers have no designated players
        else
        {
            if (collision.tag == "Player 1")
            {
                inRange1 = true;
            }
            if (collision.tag == "Player 2")
            {
                inRange2 = true;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player 1")
        {
            inRange1 = false;
        }
        if (collision.tag == "Player 2")
        {
            inRange2 = false;
        }
    }
}
