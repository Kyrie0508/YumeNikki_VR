using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 startPoint;
    public Vector3 endPoint;

    private Vector3 moveDirection;
    private float totalDistance;

    void Start()
    {
        transform.position = startPoint;
        moveDirection = (endPoint - startPoint).normalized;
        totalDistance = Vector3.Distance(startPoint, endPoint);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        float traveled = Vector3.Dot(transform.position - startPoint, moveDirection);

        if (traveled >= totalDistance)
        {
            transform.position = startPoint;
        }
    }
}
