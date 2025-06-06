using UnityEngine;

public class ChangeRespawn : MonoBehaviour
{
    public Lever lever;
    public PlayerMove player;
    public Transform newRespawn;

    private void Update()
    {
        if (lever.on)
        {
            player.respawn = newRespawn;
        }
    }
}
