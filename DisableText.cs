using UnityEngine;

public class DisableText : MonoBehaviour
{
    public Canvas canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player 1" || collision.tag == "Player 2")
        {
            canvas.enabled = false;
        }
    }
}
