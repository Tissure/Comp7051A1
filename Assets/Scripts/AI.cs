using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class AI : MonoBehaviour
{
    private NavMeshTriangulation Triangulation;
    private NavMeshAgent Agent;
    private Animator Animator;
    [SerializeField]
    AISFX audioSource;
    [SerializeField]
    [Range(0f, 3f)]
    private float WaitDelay = 1f;

    [SerializeField]
    private int defaultHealth = 5;
    [SerializeField]
    private int health;
    [SerializeField]
    private float respawnTime = 5f;

    private Vector2 Velocity;
    private Vector2 SmoothDeltaPosition;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Triangulation = NavMesh.CalculateTriangulation();
        Animator.applyRootMotion = true;
        Agent.updatePosition = false;
        Agent.updateRotation = true;
    }

    private void Start()
    {
        GoToRandomPoint();
    }

    public void GoToRandomPoint()
    {
        StartCoroutine(DoMoveToRandomPoint());
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPosition = Animator.rootPosition;
        rootPosition.y = Agent.nextPosition.y;
        transform.position = rootPosition;
        Agent.nextPosition = rootPosition;
    }

    private void Update()
    {
        SynchronizeAnimatorAndAgent();
    }

    private void SynchronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPosition = Agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;
        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        Velocity = SmoothDeltaPosition / Time.deltaTime;
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            Velocity = Vector2.Lerp(Vector2.zero, Velocity, Agent.remainingDistance);
        }

        //bool shouldMove = Velocity.magnitude > 0.5f && Agent.remainingDistance > Agent.stoppingDistance;

        //Animator.SetBool("move", shouldMove);
        Animator.SetFloat("XMove", Velocity.x);
        Animator.SetFloat("YMove", Velocity.y);

    }

    public void StopMoving()
    {
        Agent.isStopped = true;
        StopAllCoroutines();
    }

    private IEnumerator DoMoveToRandomPoint()
    {
        Agent.enabled = true;
        Agent.isStopped = false;
        WaitForSeconds Wait = new(WaitDelay);
        while (true)
        {
            int index = Random.Range(1, Triangulation.vertices.Length - 1);
            Agent.SetDestination(Triangulation.vertices[index]);

            yield return null;
            yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);
            yield return Wait;
        }
    }

    public void Damage()
    {
        health--;
        Debug.Log(health);
        if (health <= 0)
        {
            transform.position = new Vector3(0, -10, 0);
            audioSource.PlayDie();
            StopMoving();
            StartCoroutine(DelayReset());
        }
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(respawnTime);
        MazeGameManager.Instance.ResetAI();
    }

    public void ResetAI()
    {
        health = defaultHealth;
        audioSource.PlayRespawn();
        GoToRandomPoint();
    }
}