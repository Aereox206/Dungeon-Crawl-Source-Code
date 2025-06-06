using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(TilemapCollider2D))]
public class TileAccessController : MonoBehaviour
{
    // [Tooltip("Only this GameObject is allowed to collide with this tilemap.")]
    public GameObject allowedPlayer;

    private TilemapCollider2D tilemapCollider;
    private HashSet<Collider2D> ignoredColliders = new HashSet<Collider2D>();

    private void Awake()
    {
        tilemapCollider = GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
            // Debug.LogError("TileAccessController requires a TilemapCollider2D component.");
            return;
        }
        tilemapCollider.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Ground"); // Keep on Ground layer
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;
        if (otherCollider == null) return;

        if (!IsAllowed(collision.gameObject))
        {
            if (!ignoredColliders.Contains(otherCollider))
            {
                StartCoroutine(IgnoreCollisionAfterFrame(otherCollider, collision.gameObject));
            }
        }
    }

    private IEnumerator IgnoreCollisionAfterFrame(Collider2D otherCollider, GameObject obj)
    {
        yield return null; // Immediate ignore to minimize grounding window
        if (otherCollider != null && tilemapCollider != null)
        {
            Physics2D.IgnoreCollision(tilemapCollider, otherCollider, true);
            ignoredColliders.Add(otherCollider);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.AddForce(Vector2.down * 2f, ForceMode2D.Impulse); // Stronger force
            }
        }
    }

    public bool IsAllowed(GameObject obj)
    {
        if (allowedPlayer == null)
        {
            // Debug.LogWarning("AllowedPlayer is not assigned in TileAccessController.");
            return false;
        }
        return obj == allowedPlayer;
    }

    private void OnDisable()
    {
        foreach (var collider in ignoredColliders)
        {
            if (collider != null)
            {
                Physics2D.IgnoreCollision(tilemapCollider, collider, false);
            }
        }
        ignoredColliders.Clear();
        // Debug.Log("Reset all collision ignores on disable.");
    }
}