using UnityEngine;
using UnityEngine.AI;

public class NPCChaseController : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 5f;
    public float stopDistance = 10f;

    public Vector3[] patrolPoints ;
    private int currentIndex = 0;

    private NavMeshAgent agent;
    private Vector3 originPosition;

    private enum State { Idle, Chasing, Returning }
    private State currentState = State.Idle;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originPosition = transform.position;

        patrolPoints = new Vector3[]
        {
            new Vector3(45f, transform.position.y, 14f),
            new Vector3(64f, transform.position.y, 14f),
            new Vector3(64f, transform.position.y, 28f),
            new Vector3(45f, transform.position.y, 28f),
        };
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        switch (currentState)
        {
            case State.Idle:
                if (patrolPoints.Length == 0) return;
                if (!agent.pathPending && agent.remainingDistance < 0.25f)
                {
                    currentIndex = (currentIndex + 1) % patrolPoints.Length;
                    agent.SetDestination(patrolPoints[currentIndex]);
                }

                if (distance <= chaseDistance && player.position.x >= 33f)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                if (distance > stopDistance || player.position.x < 33f)
                {
                    currentState = State.Returning;
                }
                else
                {
                    agent.SetDestination(player.position);
                }
                break;

            case State.Returning:
                agent.SetDestination(originPosition);
                if (Vector3.Distance(transform.position, originPosition) < 0.3f)
                {
                    currentState = State.Idle;
                }
                break;
        }
    }
}