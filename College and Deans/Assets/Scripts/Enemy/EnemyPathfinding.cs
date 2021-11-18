using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField]private Enemy enemy;
    [SerializeField]private Transform target;

    public float speed = .02f;
    int nodoActual = 0;
    public bool reachedEndOfPath = false;
    List<Vector3> vectorPath;
    Pathfinding pathfinding;
    GameManager gameManager;
    Vector2 direction;
    
    private void Awake() 
    {
        enemy = GetComponent<Enemy>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate() 
    {
        HandleMovement();
    }

    private void HandleMovement() 
    {
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
    }

    public void MoveTo(Vector3 targetPosition)
    {
        SetTargetPosition(targetPosition);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        nodoActual = 0;
        vectorPath = pathfinding.FindPath(enemy.GetPosition(), targetPosition);

        if(vectorPath != null && vectorPath.Count > 1)
        {
            vectorPath.RemoveAt(0);
        }
    } 

    public void StopMoving()
    {
        vectorPath = null;
        enemy.EnemyRigidbody2D.velocity = Vector2.zero;
    }

    public Vector2 GetDirectionMov()
    {
        return direction;
    }

    public void SetPathfinding(Pathfinding _pathfinding)
    {
    this.pathfinding = _pathfinding;
    }
}
