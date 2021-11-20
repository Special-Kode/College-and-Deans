using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_IA : MonoBehaviour
{
    
    private enum State
    {
        Chasing,
        Embestida,
        Bofetada,
    }

    private EnemyPathfinding pathfinding;
    private Vector3 startPosition;
    private Transform target;
    private float nextAttack; 
    private float nextEmbestida;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;
    [SerializeField] private float embestidaRange;
    [SerializeField] private float embestidaCD;
    public bool embistiendo { get;  private set; }
    public bool bofetada { get;  private set; }
    public bool locked = false;
    private Vector2 landingPosition;
    [SerializeField] private float speed;
    private State state;
    private Enemy enemy;
    private void Awake() 
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<Enemy>();
        state = State.Chasing;    
    }

    void Start()
    {
        startPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nextAttack = 5;
        nextEmbestida = 5;
    }

    void Update()
    {
        switch(state)
        {
            default:
            case State.Chasing:
                pathfinding.MoveTo(target.position);
                nextAttack -= Time.smoothDeltaTime;
                nextEmbestida -= Time.smoothDeltaTime;

                if (Vector2.Distance(transform.position, target.position) <= attackRange && nextAttack < 0)
                {
                    pathfinding.StopMoving();
                    state = State.Bofetada;
                    nextAttack = attackCD;
                    StartCoroutine(Attack());
                }

                if(Vector2.Distance(transform.position, target.position) >= embestidaRange && nextEmbestida < 0 || nextEmbestida < -5 )
                {
                    pathfinding.StopMoving();
                    embistiendo = true;
                    nextEmbestida = embestidaCD;
                    state = State.Embestida;
                    StartCoroutine(Embestida());
                }
                break;
            case State.Bofetada:
                if(locked)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, landingPosition, (speed / 2) * Time.deltaTime);
                    if(Vector2.Distance(this.transform.position, landingPosition) < 0.1f)
                    {
                        //bofetada = false;
                        locked = false;
                        state = State.Chasing;
                    }
                }
                break;
            case State.Embestida:
                if(locked)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, landingPosition, speed * Time.deltaTime); 
                    if(Vector2.Distance(this.transform.position, landingPosition) < 0.1f)
                    {
                        embistiendo = false;
                        locked = false;
                        state = State.Chasing;
                    }
                }
                break;
        }
    }

    IEnumerator Attack()
    {
        landingPosition = target.position;
        enemy.Boss_2_Animation.Bofetada();
        yield return new WaitForSeconds(0.2f);
        locked = true;
    }

    IEnumerator Embestida()
    {
        yield return new WaitForSeconds(1f);
        locked = true;
        landingPosition = target.position;
    }

}
