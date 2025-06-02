using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public string spawnPointName;
        public Transform point;
    }

    public SpawnPoint[] spawnPoints;

    void Start()
    {
        GameObject xrOrigin = GameObject.Find("XR Origin");
        if (xrOrigin == null) return;

        string spawnName = SceneTransitionManager.NextSpawnPointName;

        foreach (var sp in spawnPoints)
        {
            if (sp.spawnPointName == spawnName)
            {
                xrOrigin.transform.position = sp.point.position;
                xrOrigin.transform.rotation = sp.point.rotation;
                break;
            }
        }
    }
}
