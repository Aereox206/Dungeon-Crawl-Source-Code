using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapPowerUp : MonoBehaviour
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
            player.hasSwap = true;

            if (player.playerNumber == 1)
            {
                cg1.gameObject.SetActive(true);
                cg1.alpha = 1;
                StartCoroutine(FadeOutCanvasGroup(cg1));
            }

            if (player.playerNumber == 2)
            {
                cg2.gameObject.SetActive(true);
                cg2.alpha = 1;
                StartCoroutine(FadeOutCanvasGroup(cg2));
            }

            if (collected.Count == 2)
            {
                // Disable sprite to prevent further pickup
                if (powerupSprite != null)
                    powerupSprite.enabled = false;

                // Delay destruction to allow UI to display
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
