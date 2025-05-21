using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeTarget : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;
    public AudioSource audioSource;
    public AudioClip fadeSound;
    [Range(0f, 10f)] public float effectVolume = 1f;

    [Header("Optional")]
    public AudioSource bgmSource;

    private bool isFading = false;

    public void StartFadeIn()
    {
        if (isFading) return;
        StartCoroutine(Fade(0f, 1f)); 
    }

    public void StartFadeOut()
    {
        if (isFading) return;
        StartCoroutine(Fade(1f, 0f)); 
    }

    private IEnumerator Fade(float from, float to)
    {
        isFading = true;

        if (bgmSource != null && bgmSource.isPlaying)
            bgmSource.Stop();

        if (audioSource != null && fadeSound != null)
        {
            audioSource.volume = effectVolume;
            audioSource.PlayOneShot(fadeSound);
        }

        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / fadeDuration);
            color.a = Mathf.Clamp01(alpha);
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
        isFading = false;
    }
}