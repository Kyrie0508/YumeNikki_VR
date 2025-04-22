using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Light targetLight;        // 깜빡일 조명
    public float minTime = 0.1f;     // 최소 깜빡임 간격
    public float maxTime = 0.5f;     // 최대 깜빡임 간격

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());   // 시작할 때 깜빡이기 시작
    }

    // Update는 사용하지 않아도 됨 (자동 깜빡이므로)
    void Update()
    {
        
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            targetLight.enabled = !targetLight.enabled;  // 조명 On/Off
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
