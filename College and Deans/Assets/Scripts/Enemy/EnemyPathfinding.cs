using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;

    public float speed = .02f;
    int nodoActual = 0;
    public bool reachedEndOfPath = false;
    List<Vector3> vectorPath;
    Pathfinding pathfinding;
    
    private void Awake() 
    {
        enemy = GetComponent<Enemy>();
    }


    private void Start() 
    {
        pathfinding = new Pathfinding(30, 15, 1f, new Vector3(-10f, -6f, 0));
        //target = GameObject.FindGameObjectWithTag("Player").transform;

        //vectorPath = pathfinding.FindPath(enemy.GetPosition(), target.position);
    }

    private void FixedUpdate() 
    {
        //vectorPath = pathfinding.FindPath(enemy.GetPosition(), target.position);
        HandleMovement();
    }

    private void HandleMovement() 
    {
        if(vectorPath != null){
            Vector2 targetPosition = (Vector2)vectorPath[nodoActual] + new Vector2(-10f, -6f);
            if(Vector2.Distance(enemy.GetPosition(), targetPosition) > .1f)
            {
                Vector2 direction = (targetPosition - enemy.GetPosition()).normalized;
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
            
            //Vector2 targetPosition = (Vector2)vectorPath[nodoActual] + new Vector2(-10f, -6f);
            //Vector2 direction = (targetPosition - enemy.GetPosition()).normalized;
            //Vector2 force = direction * speed * Time.deltaTime;

            //enemy.EnemyRigidbody2D.AddForce(force); 

            //float distance = Vector2.Distance(enemy.GetPosition(), targetPosition);
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        SetTargetPosition(targetPosition);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        nodoActual = 0;
        vectorPath = Pathfinding.Instance.FindPath(enemy.GetPosition(), targetPosition);

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
}
