using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public Vector3 screenPos;
    public Animator animator;
    public Rigidbody2D rb;
    public float DashSpeed;
    public bool canPass;
    public Vector2 velocity;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public float destinationReachedThreshold;
    private Vector3 target;

    private float baseAgentSpeed;
    [SerializeField] private float speedMultiplier = 1;

    public float moveSpeed = 5f;
    private bool WASD = false;
    public bool dashing = false;
    Vector2 mov;
    // Se procede a cambiar de posición al personaje dependiendo de si se mueve o procede a realizar un dash.
    void Start()
    {
        //NavMesh configuration
        agent.enabled = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        bool active = agent.isOnNavMesh;
        if (active == false)
        {

            agent.enabled = false;
            agent.enabled = true;
        }
        canPass = true;
        animator = GetComponent<Animator>();

        target = Vector3.zero;
        baseAgentSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = baseAgentSpeed * speedMultiplier;
        moveSpeed = agent.speed;

        if (WASD)
        {
            if (!dashing)
            {
                agent.enabled = false;
                mov.x = Input.GetAxisRaw("Horizontal");
                mov.y = Input.GetAxisRaw("Vertical");
                velocity.x = mov.x;
                velocity.y = mov.y;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            }

        }
        else
        {
            

            if (agent.enabled == true)
            {
                velocity = agent.velocity;

                if (target != Vector3.zero)
                    if (Vector2.SqrMagnitude(target - transform.position) < destinationReachedThreshold)
                    {
                        animator.SetBool("Walking", false);
                        target = Vector3.zero;
                    }

            }

            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        }


    }
    void FixedUpdate()
    {
        if(!dashing)
            rb.MovePosition(rb.position+mov*moveSpeed*Time.fixedDeltaTime);
    }


    public void PlayerMoved(bool isMobile)
    {
        if (isMobile)
        {
            target = screenPos;
            agent.SetDestination(screenPos);
        }
        else
        {
            WASD = true;
        }

    }
    public void PlayerDashed(Vector3 dir)
    {
        dashing = true;
        DashSpeed = 10f;
        velocity = (Vector2)dir.normalized * DashSpeed;
        this.GetComponent<Rigidbody2D>().velocity = velocity;
       
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public void MultiplySpeed(float _speedMultiplier)
    {
        speedMultiplier *= _speedMultiplier;
    }

    public void SetSpeedMultiplier(float _speed)
    {
        speedMultiplier = _speed;
    }
}
