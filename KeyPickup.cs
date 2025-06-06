using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();
        if (player != null)
        {
            player.keyCount += 1;
            Destroy(gameObject);
        }
    }
}
