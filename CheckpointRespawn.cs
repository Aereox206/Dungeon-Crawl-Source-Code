using UnityEngine;

public class CheckpointRespawn : MonoBehaviour
{
    public PlayerMove player1;
    public Transform newRespawn1;

    public PlayerMove player2;
    public Transform newRespawn2;

    private void updateRespawn()
    {
        player1.respawn = newRespawn1;
        player2.respawn = newRespawn2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player 1" || collision.tag == "Player 2")
        {
            updateRespawn();
        }
    }
}
