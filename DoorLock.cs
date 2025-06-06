using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    // public GameObject doorGraphic; // Uncomment for potentially adding animation to unlocking

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();
        if (player != null && player.keyCount > 0)
        {
            player.keyCount -= 1;
            Destroy(gameObject); 
        }
    }
}
