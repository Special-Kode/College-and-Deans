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
    [SerializeField]private Transform target;
    [SerializeField] private float fireRate;
    private float nextAttack;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speedBullet;
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

                if(Vector2.Distance(transform.position, target.position) <= attackRange)
                {
                    pathfinding.StopMoving();
                    state = State.Attacking;
                    nextAttack = Time.time + fireRate;
                }
                break;
            case State.Attacking:
                if(Time.time >= nextAttack)
                {
                    nextAttack = Time.time + fireRate;
                    GameObject tempBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
                    tempBullet.GetComponent<Rigidbody2D>().AddForce(dir * speedBullet * Time.deltaTime);
                }
                if(Vector2.Distance(transform.position, target.position) > attackRange)
                {
                    nextAttack = Time.time + fireRate;
                    state = State.Chasing;
                }
                break;
        }
    }
}
