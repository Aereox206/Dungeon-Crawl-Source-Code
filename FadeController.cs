using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Gate gate;
    public FadeToBlack FTB; 
    public GameObject player1;
    public GameObject player2;
    public bool ready;

    private bool inRange1 = false;
    private bool inRange2 = false;

    private bool inGate1 = false;
    private bool inGate2 = false;

    private bool executed = false;

    public void Update()
    {
        if (ready)
        {
            if (gate.open && inRange1 && Input.GetKeyDown(KeyCode.E))
            {
                inGate1 = true;
                player1.SetActive(false);
            }
            if (gate.open && inRange2 && Input.GetKeyDown(KeyCode.RightShift))
            {
                inGate2 = true;
                player2.SetActive(false);
            }
        }

        if (inGate1 && inGate2 && !executed)
        {
            FTB.FadeOut();
            executed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    private void OnTriggerExit2D(Collider2D collision)
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

    public bool PlayersEnteredGate()
    {
        return inGate1 && inGate2;
    }

}
