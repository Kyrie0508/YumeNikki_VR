using UnityEngine;

public class TVFlicker : MonoBehaviour
{
    public float minInterval = 0.05f; 
    public float maxInterval = 0.2f; 

    private float timer = 0f;
    private float nextBlinkTime;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        SetNextBlinkTime();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextBlinkTime)
        {
            rend.enabled = !rend.enabled;
            timer = 0f;
            SetNextBlinkTime(); 
        }
    }

    void SetNextBlinkTime()
    {
        nextBlinkTime = Random.Range(minInterval, maxInterval);
    }
}
