using UnityEngine;

public class AutoMove2 : MonoBehaviour
{
    public float speed = 3f;

    // Plane 범위에 맞게 설정
    public Vector2 mapMin = new Vector2(-250f, -250f);
    public Vector2 mapMax = new Vector2(250f, 250f);

    private Vector3 targetPosition;
    private float reachThreshold = 0.5f;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < reachThreshold)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        float x = Random.Range(mapMin.x, mapMax.x);
        float z = Random.Range(mapMin.y, mapMax.y);
        float y = transform.position.y; // 높이 유지

        targetPosition = new Vector3(x, y, z);
    }
}
