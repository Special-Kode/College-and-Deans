using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public Vector3 screenPos;
    public Animator animator;
    public float speed;
    public bool canPass;
    public Vector2 velocity;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public float destinationReachedThreshold;
    private Vector3 target;

    private float baseAgentSpeed;
    private float speedMultiplier;


    // Se procede a cambiar de posición al personaje dependiendo de si se mueve o procede a realizar un dash.
    void Start()
    {
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
        //offset = this.gameObject.transform.TransformPoint(offset, 0, 0).x;
        speedMultiplier = 1;
        baseAgentSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled == true)
        {
            velocity = agent.velocity;

            if(target != Vector3.zero)
                if (Vector2.SqrMagnitude(target - transform.position) < destinationReachedThreshold)
                {
                    animator.SetBool("Walking", false);
                    target = Vector3.zero;
                }
                    
        }
            
        
    }


    public void PlayerMoved()
    {
        target = screenPos;
        agent.SetDestination(screenPos);
       

    }
    public void PlayerDashed(Vector3 dir)
    {
            speed = 10f;
            velocity = (Vector2)dir.normalized * speed;
            this.GetComponent<Rigidbody2D>().velocity = velocity;
        


    }

    public void SetSpeedMultiplier(float _speed)
    {
        speedMultiplier = _speed;
        agent.speed = baseAgentSpeed * speedMultiplier;
    }
}
