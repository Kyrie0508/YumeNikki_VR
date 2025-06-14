using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    [Tooltip("5초 길이 발소리 클립")]
    public AudioClip footstepClip;

    private AudioSource _audio;
    private CharacterController _cc;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _cc = GetComponent<CharacterController>();

        _audio.playOnAwake = false;
        _audio.clip = footstepClip;
        _audio.loop = true;
    }

    void Update()
    {
        bool isMoving = _cc.velocity.magnitude > 0.1f;

        if (isMoving)
        {
            if (!_audio.isPlaying)
                _audio.Play();
        }
        else
        {
            if (_audio.isPlaying)
                _audio.Stop();
        }
    }
}
