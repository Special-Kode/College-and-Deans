using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Se procede a cambiar de posición al personaje dependiendo de si se mueve o procede a realizar un dash.
    void Start()
    {
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
        if (!animatorPlayer.GetBool("Attacking"))
        {
            Up = animatorPlayer.GetBool("Up");
            Down = animatorPlayer.GetBool("Down");
            Left = animatorPlayer.GetBool("Left");
            Right = animatorPlayer.GetBool("Right");

        }
       




    }


    public void PlayerMoved()
    {
        //Debug.Log(this.GetComponent<Rigidbody2D>().velocity.magnitude);
            speed = 10f;
            screenPos.z = 0;
            Vector3 dir = screenPos - transform.position;
            velocity = dir.normalized * speed;
            this.GetComponent<Rigidbody2D>().velocity = velocity;


        //  Collider2D[] collider = Physics2D.OverlapBoxAll(new Vector2(newPosition.x, newPosition.y), new Vector2(2.56f/2f, 2.56f/2f), 0,mask);
        /*if (collider.Length < 1 && Vector3.Distance(transform.position, screenPos) > 0.05f)
        {
           
            transform.position = newPosition;
        }*/






        /*
        
        if (Up)
        {
            if (Left || Right)
                movement.y = Mathf.Sqrt(0.00125f);
            else
                movement.y = 0.05f;
            player.transform.position=applyMovement(movement, offsetx,offsety);
           

        }
         if (Down)
        {
            if (Left || Right)
                movement.y = -Mathf.Sqrt(0.00125f);
            else
                movement.y = -0.05f;
            player.transform.position = applyMovement(movement, offsetx, offsety);
        }
        if (Left)
        {
            if (Up || Down)
                movement.x = -Mathf.Sqrt(0.00125f);
            else
                 movement.x = -0.05f;
            player.transform.position = applyMovement(movement, offsetx, offsety);

        }
         if (Right)
        {
            if (Up || Down)
                movement.x = Mathf.Sqrt(0.00125f);
            else
                 movement.x = 0.05f;
            player.transform.position = applyMovement(movement, offsetx,offsety);
        }

       */



    }
    public void PlayerDashed()
    {
            speed = 20f;
            direction = (playerScript.posFinalDash - playerScript.PosInitDash);
            velocity = direction.normalized * speed;
            this.GetComponent<Rigidbody2D>().velocity = velocity;



    }

  /*  private bool applyMovement(Vector3 newPosition)
    {
        if (this.GetComponent<ExternMechanicsPlayer>().MoveOrNot(newPosition))
            return true;
        else
            return false;
      
    }*/
}
