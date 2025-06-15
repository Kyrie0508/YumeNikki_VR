using UnityEngine;
using UnityEngine.AI;

public class NPCChaseController : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 5f;
    public float stopDistance = 7.5f;

    public Vector3[] patrolPoints ;
    private int currentIndex = 0;

    private NavMeshAgent agent;
    private Vector3 originPosition;

    public enum NPCState { Idle, Chasing, Returning, Talking }
    public NPCState currentState = NPCState.Idle;
    private Animator animator;

    public float maxBlur = 5f;
    public float duration = 5f;
    public Material blurMaterial;
    public ClassDialogueTrigger classDialogueTrigger;
    public SuperBlurController blurController;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originPosition = transform.position;

        animator.SetBool("isWalk", true);

        patrolPoints = new Vector3[]
        {
            new Vector3(45f, transform.position.y, 14f),
            new Vector3(64f, transform.position.y, 14f),
            new Vector3(64f, transform.position.y, 28f),
            new Vector3(45f, transform.position.y, 28f),
        };

        if (blurController == null)
            blurController = GetComponent<SuperBlurController>();
        
        if (classDialogueTrigger == null)
            classDialogueTrigger = GetComponent<ClassDialogueTrigger>();
    }

    void Update()
    {        
        float distance = Vector3.Distance(player.position, transform.position);

        switch (currentState)
        {
            case NPCState.Idle:
                if (patrolPoints.Length == 0) return;
                if (!agent.pathPending && agent.remainingDistance < 0.25f)
                {
                    currentIndex = (currentIndex + 1) % patrolPoints.Length;
                    agent.SetDestination(patrolPoints[currentIndex]);
                }

                if (distance <= chaseDistance && player.position.x >= 34f)
                {
                    currentState = NPCState.Chasing;
                }
                break;

            case NPCState.Chasing:
                if (distance < 3f)
                {
                    agent.SetDestination(transform.position);
                    currentState = NPCState.Talking;
                }
                else if (distance > stopDistance || player.position.x < 34f)
                {
                    currentState = NPCState.Returning;
                }
                else
                {
                    agent.SetDestination(player.position);
                }

                break;

            case NPCState.Returning:
                agent.SetDestination(originPosition);
                if (Vector3.Distance(transform.position, originPosition) < 0.3f)
                {
                    currentState = NPCState.Idle;
                }
                break;
            
            case NPCState.Talking:
                agent.isStopped = true;
                animator.SetBool("isWalk", false);
                classDialogueTrigger.ShowDialogue();
                blurController.TriggerBlur();
                return;
        }
    }
}