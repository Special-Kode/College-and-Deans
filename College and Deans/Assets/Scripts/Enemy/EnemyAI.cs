using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private enum State
    {
        Chasing,
        Attacking,
        Stop
    }

    private enum Tipo
    {
        Melee_1,
        Melee_2,
        Distancia_1,
        Distancia_2
    }

    private EnemyPathfinding pathfinding;
    private Vector3 startPosition;
    [SerializeField] private Transform target;
    [SerializeField] private float fireRate;
    [SerializeField] private float nextAttack;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speedBullet;
    public bool attacking;
    public bool locked = false;
    private Vector2 landingPosition;
    [SerializeField] private float speed;

    private State state;
    [SerializeField] Tipo tipo;

    private void Awake() 
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Chasing;    
    }

    void Start()
    {
        startPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nextAttack = fireRate;
    }

    void Update()
    {
        switch(state)
        {
            default:
            case State.Chasing:
                pathfinding.MoveTo(target.position);
                nextAttack -= Time.smoothDeltaTime;

                if(Vector2.Distance(transform.position, target.position) <= attackRange && tipo == Tipo.Distancia_1 )
                {
                    pathfinding.StopMoving();
                    state = State.Attacking;
                    nextAttack = fireRate;
                    StartCoroutine(Attack());
                }
                if(nextAttack < 0 && tipo == Tipo.Distancia_2)
                {
                    pathfinding.StopMoving();
                    state = State.Attacking;
                    nextAttack = fireRate;
                    StartCoroutine(Attack());
                }
                if(Vector2.Distance(transform.position, target.position) <= attackRange && nextAttack < 0 && (tipo == Tipo.Melee_1 || tipo == Tipo.Melee_2))
                {
                    pathfinding.StopMoving();
                    state = State.Attacking;
                    nextAttack = fireRate;
                    StartCoroutine(Attack());
                }

                break;
            case State.Attacking:
                switch(tipo)
                {
                    case Tipo.Distancia_1:
                        if(nextAttack < 0)
                        {
                            nextAttack = fireRate;
                            GameObject tempBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                            Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
                            tempBullet.GetComponent<Rigidbody2D>().velocity = dir * speedBullet;
                        }
                        if(Vector2.Distance(transform.position, target.position) > attackRange)
                        {
                            nextAttack = fireRate;
                            state = State.Chasing;
                            attacking = false;
                        }
                        nextAttack -= Time.smoothDeltaTime;
                        break;
                    case Tipo.Distancia_2:
                        if(nextAttack < 0)
                        {
                            nextAttack = fireRate;
                            GameObject bullet0 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                            bullet0.GetComponent<Rigidbody2D>().velocity = Vector2.up * speedBullet;
                            GameObject bullet1 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                            bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.right * speedBullet;
                            GameObject bullet2 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                            bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.down * speedBullet;
                            GameObject bullet3 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                            bullet3.GetComponent<Rigidbody2D>().velocity = Vector2.left * speedBullet;

                            state = State.Chasing;
                            attacking = false;
                        }
                        nextAttack -= Time.smoothDeltaTime;
                        break;
                    case Tipo.Melee_1:
                        if(locked)
                        {
                            transform.position = Vector2.MoveTowards(this.transform.position, landingPosition, speed * Time.deltaTime);
                            if(Vector2.Distance(this.transform.position, landingPosition) < 0.1f)
                            {
                                locked = false;
                                attacking = false;
                                state = State.Chasing;
                            }
                        }
                        break;
                    case Tipo.Melee_2:
                        if(locked)
                        {
                            transform.position = Vector2.MoveTowards(this.transform.position, landingPosition, speed * Time.deltaTime);
                            if(Vector2.Distance(this.transform.position, landingPosition) < 0.1f)
                            {
                                locked = false;
                                attacking = false;
                                state = State.Chasing;
                            }
                        }
                        break;
                }
                break;
            case State.Stop:
                pathfinding.StopMoving();
                break;
        }
    }

    public void PararEnemigo()
    {
        state = State.Stop;
    }
    public void MoverEnemigo()
    {
        state = State.Chasing;
    }

    IEnumerator Attack()
    {
        landingPosition = target.position;
        yield return new WaitForSeconds(0.2f);
        attacking = true;
        locked = true;
    }
}
