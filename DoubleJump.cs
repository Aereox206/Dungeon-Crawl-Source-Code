using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoubleJumpPowerUp : MonoBehaviour
{
    public CanvasGroup cg1, cg2;
    public SpriteRenderer powerupSprite;

    private HashSet<int> collected = new HashSet<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove player = collision.GetComponent<PlayerMove>();
        if (player != null && !collected.Contains(player.playerNumber))
        {
            collected.Add(player.playerNumber);
            player.hasDoubleJump = true;
            player.canDoubleJump = true;

            if (player.playerNumber == 1)
            {
                cg1.alpha = 1;
                StartCoroutine(FadeOutCanvasGroup(cg1));
            }
            else if (player.playerNumber == 2)
            {
                cg2.alpha = 1;
                StartCoroutine(FadeOutCanvasGroup(cg2));
            }

            if (collected.Count == 2)
            {
                if (powerupSprite != null) powerupSprite.enabled = false;
                StartCoroutine(DelayedDestroy(5f));
            }
        }
    }

    private IEnumerator FadeOutCanvasGroup(CanvasGroup cg)
    {
        float duration = 5f;
        float t = 0f;
        while (t < duration)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 0f;
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
