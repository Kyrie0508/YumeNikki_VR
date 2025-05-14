using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcWalk : MonoBehaviour
{
    public Vector3[] points;
    public float speed = 1.75f;
    public float arriveThreshold = 0.25f;
    private int currentIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (points.Length == 0) return;

        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 currentTarget = points[currentIndex];
        Vector3 flatPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatTarget = new Vector3(currentTarget.x, 0f, currentTarget.z);
        float distance = Vector3.Distance(flatPos, flatTarget);

        if (distance < arriveThreshold)
        {
            transform.Rotate(0, -90f, 0);
            currentIndex = (currentIndex + 1) % points.Length;
        }
    }
}
