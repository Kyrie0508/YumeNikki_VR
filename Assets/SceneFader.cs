using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float time = fadeDuration;
        Color c = fadeImage.color;
        while (time > 0)
        {
            time -= Time.deltaTime;
            c.a = time / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0;
        fadeImage.color = c;
    }

    IEnumerator FadeOut(string sceneName)
    {
        float time = 0;
        Color c = fadeImage.color;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            c.a = time / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }
        c.a = 1;
        fadeImage.color = c;
        SceneManager.LoadScene(sceneName);
    }
}
