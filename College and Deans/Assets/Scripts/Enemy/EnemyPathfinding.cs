using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField]private Enemy enemy;

    public float speed = .02f;
    int nodoActual = 0;
    public bool reachedEndOfPath = false;
    List<Vector3> vectorPath;
    Pathfinding pathfinding;
    GameManager gameManager;
    Vector2 direction;
    private NavMeshAgent agent;
    
    private void Awake() 
    {
        enemy = GetComponent<Enemy>();
        gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void FixedUpdate() 
    {
        HandleMovement();
    }

    private void HandleMovement() 
    {
        /*
        if(vectorPath != null){
            Vector2 targetPosition = (Vector2)vectorPath[nodoActual] + pathfinding.GetOriginPosition();
            if(Vector2.Distance(enemy.GetPosition(), targetPosition) > .1f)
            {
                direction = (targetPosition - enemy.GetPosition()).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                enemy.EnemyRigidbody2D.AddForce(force); 
            }else
            {
                nodoActual++;
                if(nodoActual >= vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    StopMoving();
                }else
                {
                    reachedEndOfPath = false;
                }
            }
        }
        */
        if(agent.enabled)
        {
            agent.speed = speed;
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        SetTargetPosition(targetPosition);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        /*
        nodoActual = 0;
        vectorPath = pathfinding.FindPath(enemy.GetPosition(), targetPosition);

        if(vectorPath != null && vectorPath.Count > 1)
        {
            vectorPath.RemoveAt(0);
        }
        */
        agent.SetDestination(targetPosition);
    } 

    public void StopMoving()
    {
        /*
        vectorPath = null;
        enemy.EnemyRigidbody2D.velocity = Vector2.zero;
        */
        agent.ResetPath();
    }

    public Vector2 GetDirectionMov()
    {
        direction = ((Vector2)enemy.EnemyAI.GetTargetPosition() - enemy.GetPosition()).normalized;
        return direction;
    }

    public void SetPathfinding(Pathfinding _pathfinding)
    {
        this.pathfinding = _pathfinding;
    }
}