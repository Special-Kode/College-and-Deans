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
    [SerializeField] private float attackCD;
    [SerializeField] public bool isJumping { get;  private set; }
    private float timeBeforeAttack;
    private int counter = 0;
    [SerializeField] private float speed;
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
        nextAttack = 5;
    }

    void Update()
    {
        switch(state)
        {
            default:
            case State.Chasing:
                pathfinding.MoveTo(target.position);
                nextAttack -= Time.smoothDeltaTime;

                if (nextAttack < 0)
                {
                    pathfinding.StopMoving();
                    int random = Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            state = State.Attacking;
                            nextAttack = attackCD;
                            timeBeforeAttack = fireRate;
                            break;
                        case 1:
                            state = State.Jumping;
                            break;
                    }
                }
                break;
            case State.Attacking:
                if(timeBeforeAttack < 0)
                {
                    GameObject bullet0 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet0.GetComponent<Rigidbody2D>().velocity = Vector2.up * speedBullet;
                    GameObject bullet1 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f).normalized * speedBullet;
                    GameObject bullet2 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.right * speedBullet;
                    GameObject bullet3 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, -1f).normalized * speedBullet;
                    GameObject bullet4 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet4.GetComponent<Rigidbody2D>().velocity = Vector2.down * speedBullet;
                    GameObject bullet5 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet5.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, -1f).normalized * speedBullet;
                    GameObject bullet6 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet6.GetComponent<Rigidbody2D>().velocity = Vector2.left * speedBullet;
                    GameObject bullet7 = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    bullet7.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 1f).normalized * speedBullet;

                    timeBeforeAttack = fireRate;
                    counter++;
                }

                timeBeforeAttack -= Time.deltaTime;

                if (counter == 3)
                {
                    Debug.Log("voy a perseguir");
                    counter = 0;
                    state = State.Chasing;
                }
                break;
            case State.Jumping:
                StartCoroutine(Jump());
                break;
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;
        bool hasJumped = false;
        float debug = 0;
        Vector2 firstTarget = (Vector2)this.transform.position + Vector2.up;
        while (!hasJumped && debug < 3)
        {
            debug += 1f;
            Vector2.MoveTowards(this.transform.position, firstTarget, speed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, firstTarget) < 0.1f)
            {
                hasJumped = true;
            }
        }
        Vector2 secondTarget = target.position;
        yield return new WaitForSeconds(1);
        while (isJumping)
        {
            Vector2.MoveTowards(this.transform.position, secondTarget, speed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, secondTarget) < 0.1f)
            {
                isJumping = false;
            }
        }
    }
}
