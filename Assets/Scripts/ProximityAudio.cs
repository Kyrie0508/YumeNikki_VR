using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 15f;
    public float minVolume = 0f;
    public float maxVolume = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < maxDistance)
        {
            float t = 1 - (distance / maxDistance);
            float volume = Mathf.Lerp(minVolume, maxVolume, t);
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}
