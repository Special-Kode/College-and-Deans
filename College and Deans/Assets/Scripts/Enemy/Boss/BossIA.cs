using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    
    private enum State
    {
        Chasing,
        Attacking,
        Jumping
    }

    private EnemyPathfinding pathfinding;
    private Vector3 startPosition;
    private Transform target;
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
                    GameObject bullet0 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speedBullet * Time.deltaTime);
                    GameObject bullet1 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 0.5f) * speedBullet * Time.deltaTime);
                    GameObject bullet2 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speedBullet * Time.deltaTime);
                    GameObject bullet3 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, -0.5f) * speedBullet * Time.deltaTime);
                    GameObject bullet4 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speedBullet * Time.deltaTime);
                    GameObject bullet5 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, -0.5f) * speedBullet * Time.deltaTime);
                    GameObject bullet6 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speedBullet * Time.deltaTime);
                    GameObject bullet7 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 0.5f) * speedBullet * Time.deltaTime);
                }
                if(Vector2.Distance(transform.position, target.position) > attackRange)
                {
                    nextAttack = Time.time + fireRate;
                    state = State.Chasing;
                }
                break;
            //case State.Jumping:
        }
    }
}
