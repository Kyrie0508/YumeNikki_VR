// 📦 VR TV 자동 지직 효과 스크립트 (주기적으로 화면 깜빡임)
// Unity 2022.3 + XR Interaction Toolkit + OpenXR 기반

using UnityEngine;

public class PhotoActivator : MonoBehaviour
{
    [Header("🔌 이펙트 오브젝트들")]
    public GameObject sparkEffect;         // 지직거리는 전기 효과 (Particle System 등)
    public AudioSource buzzSound;          // 지직 소리
    public GameObject screenOnObject;      // 켜지는 화면 또는 UI
    public Renderer tvScreenRenderer;      // TV 화면에 이미지 적용용 Renderer
    public Texture glitchTexture;          // 치지직 이미지 (노이즈)
    public Texture normalTexture;          // 정상 TV 화면 이미지

    private Material tvMat;
    private float timer;
    public float glitchInterval = 5f;       // 몇 초마다 지직거릴지 설정
    private bool isGlitching = false;

    void Start()
    {
        // TV 화면 머티리얼 저장
        if (tvScreenRenderer != null)
        {
            tvMat = tvScreenRenderer.material;
            if (normalTexture != null)
                tvMat.mainTexture = normalTexture;
        }

        // 시작 시 이펙트 끔
        if (sparkEffect) sparkEffect.SetActive(false);
        if (screenOnObject) screenOnObject.SetActive(false);

        timer = glitchInterval;
    }

    void Update()
    {
        if (isGlitching) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TriggerGlitch();
            timer = glitchInterval + Random.Range(-1f, 1f); // 약간의 랜덤성 추가
        }
    }

    void TriggerGlitch()
    {
        isGlitching = true;

        if (sparkEffect) sparkEffect.SetActive(true);
        if (buzzSound) buzzSound.Play();
        if (screenOnObject) screenOnObject.SetActive(true);

        if (tvMat != null && glitchTexture != null)
        {
            tvMat.mainTexture = glitchTexture;
            Invoke("RestoreTVTexture", 0.5f); // 0.5초 후 복구
        }
    }

    void RestoreTVTexture()
    {
        if (tvMat != null && normalTexture != null)
        {
            tvMat.mainTexture = normalTexture;
        }

        if (sparkEffect) sparkEffect.SetActive(false);
        isGlitching = false;
    }
}