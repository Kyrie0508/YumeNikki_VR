using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class TVFlickerPlane : MonoBehaviour
{
    public Texture2D noiseImage;        // jpg ≈ÿΩ∫√≥
    public float minInterval = 0.1f;
    public float maxInterval = 0.3f;

    private MeshRenderer rend;
    private Material mat;
    private AudioSource audioSrc;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        audioSrc = GetComponent<AudioSource>();

       
        mat = new Material(Shader.Find("Unlit/Texture"));
        mat.mainTexture = noiseImage;
        rend.material = mat;

        rend.enabled = false;
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            bool on = Random.value > 0.5f;

            rend.enabled = on;

            if (on) audioSrc.UnPause();
            else audioSrc.Pause();

            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}
