using UnityEngine;

public class SimpleXRSpawner : MonoBehaviour
{
    public Transform spawnFromDefault;
    public Transform spawnFromRoom1;

    void Start()
    {
        GameObject xrOrigin = GameObject.Find("XR Origin");
        if (xrOrigin == null) return;

        // 기본 스폰 위치
        Transform targetSpawn = spawnFromDefault;

        // Room1에서 돌아왔을 경우, 해당 위치 사용
        if (SceneTransitionManager.LastSceneName == "Room1")
        {
            targetSpawn = spawnFromRoom1;
        }

        // XR Origin 위치 및 회전 적용
        xrOrigin.transform.position = targetSpawn.position;
        xrOrigin.transform.rotation = targetSpawn.rotation;
    }
}
