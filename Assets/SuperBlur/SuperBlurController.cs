// SuperBlurController.cs
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;

public class SuperBlurController : MonoBehaviour
{
    public static SuperBlurController Instance { get; private set; }

    [Header("Blur Settings")]
    public float maxRadius = 5f;
    public float duration = 3f;

    public Material blurMaterial;
    private FadeController fadeController;
    private Coroutine currentRoutine;
    public string nextSceneName = "Ending Outside";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
    }

    public IEnumerator TriggerBlur()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float radius = Mathf.Lerp(0f, maxRadius, t);
            blurMaterial.SetFloat("_Radius", radius);
            elapsed += Time.deltaTime;
            yield return null;
        }

        blurMaterial.SetFloat("_Radius", maxRadius);
        yield return new WaitForSeconds(3f);

        if (fadeController != null)
            fadeController.FadeOut();
            yield return new WaitForSeconds(3.75f);
            SceneManager.LoadScene(nextSceneName);
    }
}