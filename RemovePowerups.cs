using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public PlayerMove player1;
    public PlayerMove player2;

    public bool removeDoubleJump;
    public CanvasGroup cg1;
    public CanvasGroup cg2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player 1" || collision.tag == "Player 2")
        {
            if (removeDoubleJump)
            {
                player1.hasDoubleJump = false;
                player2.hasDoubleJump = false;
                cg1.alpha = 0;
                cg2.alpha = 0;
            }
        }
    }
}
