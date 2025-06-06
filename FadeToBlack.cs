using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float fadeDuration = 0.5f;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}
