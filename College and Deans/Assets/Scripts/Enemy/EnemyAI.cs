using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private enum State
    {
        Chasing,
        Attacking,
    }

    private EnemyPathfinding pathfinding;
    private Vector3 startPosition;
    private Transform target;
    [SerializeField] private float fireRate;
    private float nextAttack;
    [SerializeField] private float attackRange;
    private State state;

    private void Awake() 
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Chasing;    
    }

    void Start()
    {
        startPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch(state)
        {
            default:
            case State.Chasing:
                pathfinding.MoveTo(target.position);

                if(Vector2.Distance(transform.position, target.position) < attackRange)
                {
                    //Target esta en rango
                    if(Time.time > nextAttack)
                    {
                        pathfinding.StopMoving();
                        state = State.Attacking;
                        nextAttack = Time.time + fireRate;
                    }
                }
                break;
            case State.Attacking:
                if(Vector2.Distance(transform.position, target.position) > attackRange)
                {
                    state = State.Chasing;
                }
                break;
        }
    }
}
