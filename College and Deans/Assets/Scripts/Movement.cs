using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public int idImage;
    public string movementState;
    private Animator animatorPlayer;
    public bool Up,Right,Left,Down,Attack;
    private Vector3 movement;
    private GameObject player;
    private AnimatorPlayerScript playerScript;
    public Vector3 positionToMove,direction,screenPos;
    public float speed;
    public float distanceDashed;
    public Vector3 InitialPos;
   public Vector3 newPosition;
    public Vector3 oldPosition;
    public bool canPass;
    private int mask;
    public Vector2 velocity;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public GameObject DashCollider;
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
        player = this.gameObject;
        animatorPlayer = GetComponent<Animator>();
        playerScript = player.GetComponent<AnimatorPlayerScript>();
        movement = new Vector3 (0,0,0);
        distanceDashed = 0;
        InitialPos = Vector3.zero;
        oldPosition = Vector3.zero;
        canPass = true;
        mask = LayerMask.GetMask("Colliders");
        //offset = this.gameObject.transform.TransformPoint(offset, 0, 0).x;

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled == true)
            velocity = agent.velocity;
    }


    public void PlayerMoved()
    {
        if(agent.enabled==true)
            agent.SetDestination(screenPos);
       

    }
    public void PlayerDashed(Vector3 dir)
    {
            speed = 10f;
            velocity = (Vector2)dir.normalized * speed;
            this.GetComponent<Rigidbody2D>().velocity = velocity;
        


    }


}
